extends Node3D

@export var cameraSens = 0.004
@export var rotateAcceleration := 12.0
@export var rotateFriction := 14.0
@export var maxRotateSpeed := 4.0
@export var acceleration := 8.0    
@export var friction := 10.0       
@export var maxSpeed := 12.0
@export var maxZoom = 40
@export var minZoom = 0
@export var edgeSize := 5         
@export var edgeSpeedMultiplier := 1.0
var velocity: Vector3 = Vector3.ZERO
var rotate_velocity := 0.0
var rotate_input := 0.0
@onready var camera_3d: Camera3D = $Camera3d

func _ready() -> void:
	Input.mouse_mode = Input.MOUSE_MODE_VISIBLE

func _input(event):
	if event.is_action_pressed("ui_cancel"): 
		get_tree().quit()
	
	if event is InputEventMouseMotion and Input.is_action_pressed("mouse_right"):
		rotate_input = -event.relative.x * cameraSens * 100.0
		Input.mouse_mode = Input.MOUSE_MODE_CAPTURED
	if Input.is_action_just_released("mouse_right"):
		Input.mouse_mode = Input.MOUSE_MODE_VISIBLE
	_camera_zoom()
	
func _camera_zoom():
	var zoomChange = 0
	if Input.is_action_pressed("mouse_wheel_up"):
		zoomChange -= 1
	elif Input.is_action_pressed("mouse_wheel_down"):
		zoomChange += 1
	
	camera_3d.size += zoomChange
	camera_3d.size = clamp(camera_3d.size, minZoom, maxZoom)
	
func _physics_process(delta: float) -> void:
	_camera_movement(delta)
	_camera_rotation(delta)
	
func _camera_movement(delta: float):
	var input_dir = Vector2.ZERO

	input_dir.y = Input.get_axis("moveForward", "moveBackward")
	input_dir.x = Input.get_axis("moveLeft", "moveRight")

	var mouse_pos = get_viewport().get_mouse_position()
	var screen_size = get_viewport().get_visible_rect().size

	if mouse_pos.x < edgeSize:
		input_dir.x -= 1
	elif mouse_pos.x > screen_size.x - edgeSize:
		input_dir.x += 1

	if mouse_pos.y < edgeSize:
		input_dir.y -= 1
	elif mouse_pos.y > screen_size.y - edgeSize:
		input_dir.y += 1

	input_dir = input_dir.normalized()

	var move_dir = (global_basis * Vector3(input_dir.x, 0, input_dir.y)).normalized()

	if input_dir != Vector2.ZERO:
		var target_velocity = move_dir * maxSpeed
		velocity = velocity.lerp(target_velocity, acceleration * delta)
	else:
		velocity = velocity.lerp(Vector3.ZERO, friction * delta)

	global_position += velocity * delta

func _camera_rotation(delta: float):
	if rotate_input != 0.0:
		var target_speed = clamp(rotate_input, -maxRotateSpeed, maxRotateSpeed)
		rotate_velocity = lerp(rotate_velocity, target_speed, rotateAcceleration * delta)
	else:
		rotate_velocity = lerp(rotate_velocity, 0.0, rotateFriction * delta)

	rotation.y += rotate_velocity * delta

	rotate_input = 0.0
