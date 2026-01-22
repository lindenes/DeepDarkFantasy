#Makefile

install-addons:
	@echo "Downloading addons..."
	if not exist addons mkdir addons
	cd addons && \
	git clone --branch v3.9.1 https://github.com/nathanhoad/godot_dialogue_manager.git && \
	xcopy /E /I /Y godot_dialogue_manager\addons\dialogue_manager .\dialogue_manager\ && \
	rmdir /S /Q godot_dialogue_manager
	@echo "Done!"
    
clean:
	rmdir /S /Q addons

