using System.Linq;
using System.Threading.Tasks;
using Godot;
using DialogueManagerRuntime;
using Godot.Collections;
using System;

namespace DeepDarkFantasy.lib;

public static class DialogUtils
{
    public record DialogSpeaker(string name, string text);

    public record PlayerResponse(string text, string nextId);
    
    public static async Task<(DialogSpeaker speaker, PlayerResponse[] responses)>  GetNextDialogue(Resource dialog)
    {
        return await GetNextDialogue(dialog, "start");
    }
    
    public static async Task<(DialogSpeaker speaker, PlayerResponse[] responses)> GetNextDialogue(Resource dialog, string nextId)
    {
        var dialogueLine = await DialogueManager.GetNextDialogueLine(dialog, nextId);

        if (dialogueLine.Responses.Count == 0)
        {
            return (
                new DialogSpeaker(dialogueLine.Character, dialogueLine.Text), 
                [new PlayerResponse("(Дальше)", dialogueLine.NextId)]
            );
        }
     
        return (
            new DialogSpeaker(dialogueLine.Character, dialogueLine.Text),
            dialogueLine.Responses.Select(value => 
                new PlayerResponse(value.Text, value.NextId)
            ).ToArray()
        );
    }

    public static void AddNewDialogItems(DialogSpeaker speaker, PlayerResponse[] responses, Control speakerPanel, VBoxContainer playerResponsesContainer, Resource dialog, Action endFunction)
    {
        foreach (Node child in playerResponsesContainer.GetChildren())
        {
        	child.QueueFree();
        }
        
        RichTextLabel speakerLabel = speakerPanel.GetNode<RichTextLabel>("SpeakerLabel");
        RichTextLabel speakerNameLabel = speakerPanel.GetNode<RichTextLabel>("SpeakerNameLabel");
        speakerNameLabel.Text = speaker.name;
        speakerLabel.Text = speaker.text;
        
        foreach (var playerResponse in responses)
        {
            playerResponsesContainer.AddChild(PlayerDialogText.Create(playerResponse.text, async () =>
            {
                if (playerResponse.text == "(Закончить)")
                {
                    endFunction();
                }
                else
                {
                    var (speaker, responses) = await GetNextDialogue(dialog, playerResponse.nextId);
                    AddNewDialogItems(speaker, responses, speakerPanel, playerResponsesContainer, dialog, endFunction);
                }
            }));
        }
    }
}