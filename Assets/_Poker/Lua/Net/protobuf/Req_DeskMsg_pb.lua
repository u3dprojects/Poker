-- Generated By protoc-gen-lua Do not Edit
local protobuf = require "protobuf"
module('request/Req_DeskMsg_pb')


local ENTERDESK = protobuf.Descriptor();
local ENTERDESK_DESKID_FIELD = protobuf.FieldDescriptor();
local EXITDESK = protobuf.Descriptor();
local EXITDESK_DESKID_FIELD = protobuf.FieldDescriptor();

ENTERDESK_DESKID_FIELD.name = "deskId"
ENTERDESK_DESKID_FIELD.full_name = ".com.sojoys.chess.game.protobuf.request.EnterDesk.deskId"
ENTERDESK_DESKID_FIELD.number = 1
ENTERDESK_DESKID_FIELD.index = 0
ENTERDESK_DESKID_FIELD.label = 2
ENTERDESK_DESKID_FIELD.has_default_value = false
ENTERDESK_DESKID_FIELD.default_value = 0
ENTERDESK_DESKID_FIELD.type = 5
ENTERDESK_DESKID_FIELD.cpp_type = 1

ENTERDESK.name = "EnterDesk"
ENTERDESK.full_name = ".com.sojoys.chess.game.protobuf.request.EnterDesk"
ENTERDESK.nested_types = {}
ENTERDESK.enum_types = {}
ENTERDESK.fields = {ENTERDESK_DESKID_FIELD}
ENTERDESK.is_extendable = false
ENTERDESK.extensions = {}
EXITDESK_DESKID_FIELD.name = "deskId"
EXITDESK_DESKID_FIELD.full_name = ".com.sojoys.chess.game.protobuf.request.ExitDesk.deskId"
EXITDESK_DESKID_FIELD.number = 1
EXITDESK_DESKID_FIELD.index = 0
EXITDESK_DESKID_FIELD.label = 2
EXITDESK_DESKID_FIELD.has_default_value = false
EXITDESK_DESKID_FIELD.default_value = 0
EXITDESK_DESKID_FIELD.type = 5
EXITDESK_DESKID_FIELD.cpp_type = 1

EXITDESK.name = "ExitDesk"
EXITDESK.full_name = ".com.sojoys.chess.game.protobuf.request.ExitDesk"
EXITDESK.nested_types = {}
EXITDESK.enum_types = {}
EXITDESK.fields = {EXITDESK_DESKID_FIELD}
EXITDESK.is_extendable = false
EXITDESK.extensions = {}

EnterDesk = protobuf.Message(ENTERDESK)
ExitDesk = protobuf.Message(EXITDESK)

