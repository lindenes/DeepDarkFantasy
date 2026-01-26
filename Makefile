#Makefile

addons: addons/mkdir addons/dialogue_manager
	@echo "Addons ready!"

addons/dialogue_manager:
	@echo "Downloading godot_dialogue_manager..."
	cd addons && \
	git clone --branch v3.9.1 https://github.com/nathanhoad/godot_dialogue_manager.git && \
	xcopy /E /I /Y godot_dialogue_manager\addons\dialogue_manager .\dialogue_manager\ && \
	rmdir /S /Q godot_dialogue_manager

addons/mkdir:
	@if not exist addons mkdir addons
    
clean:
	rmdir /S /Q addons

