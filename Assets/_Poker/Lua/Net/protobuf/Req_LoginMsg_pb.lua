-- Generated By protoc-gen-lua Do not Edit
local protobuf = require "protobuf.protobuf"
module('request.Req_LoginMsg_pb')


local ENTERGAME = protobuf.Descriptor();
local ENTERGAME_TOKEN_FIELD = protobuf.FieldDescriptor();
local HEARTBEAT = protobuf.Descriptor();

ENTERGAME_TOKEN_FIELD.name = "token"
ENTERGAME_TOKEN_FIELD.full_name = ".com.sojoys.chess.game.protobuf.request.EnterGame.token"
ENTERGAME_TOKEN_FIELD.number = 1
ENTERGAME_TOKEN_FIELD.index = 0
ENTERGAME_TOKEN_FIELD.label = 2
ENTERGAME_TOKEN_FIELD.has_default_value = false
ENTERGAME_TOKEN_FIELD.default_value = ""
ENTERGAME_TOKEN_FIELD.type = 9
ENTERGAME_TOKEN_FIELD.cpp_type = 9

ENTERGAME.name = "EnterGame"
ENTERGAME.full_name = ".com.sojoys.chess.game.protobuf.request.EnterGame"
ENTERGAME.nested_types = {}
ENTERGAME.enum_types = {}
ENTERGAME.fields = {ENTERGAME_TOKEN_FIELD}
ENTERGAME.is_extendable = false
ENTERGAME.extensions = {}
HEARTBEAT.name = "Heartbeat"
HEARTBEAT.full_name = ".com.sojoys.chess.game.protobuf.request.Heartbeat"
HEARTBEAT.nested_types = {}
HEARTBEAT.enum_types = {}
HEARTBEAT.fields = {}
HEARTBEAT.is_extendable = false
HEARTBEAT.extensions = {}

EnterGame = protobuf.Message(ENTERGAME)
Heartbeat = protobuf.Message(HEARTBEAT)

