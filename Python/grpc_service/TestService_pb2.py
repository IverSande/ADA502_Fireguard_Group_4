# -*- coding: utf-8 -*-
# Generated by the protocol buffer compiler.  DO NOT EDIT!
# NO CHECKED-IN PROTOBUF GENCODE
# source: TestService.proto
# Protobuf Python Version: 5.29.0
"""Generated protocol buffer code."""
from google.protobuf import descriptor as _descriptor
from google.protobuf import descriptor_pool as _descriptor_pool
from google.protobuf import runtime_version as _runtime_version
from google.protobuf import symbol_database as _symbol_database
from google.protobuf.internal import builder as _builder
_runtime_version.ValidateProtobufRuntimeVersion(
    _runtime_version.Domain.PUBLIC,
    5,
    29,
    0,
    '',
    'TestService.proto'
)
# @@protoc_insertion_point(imports)

_sym_db = _symbol_database.Default()




DESCRIPTOR = _descriptor_pool.Default().AddSerializedFile(b'\n\x11TestService.proto\"\x1e\n\x0b\x44\x61taRequest\x12\x0f\n\x07\x64\x61ta_id\x18\x01 \x01(\t\"#\n\x0c\x44\x61taResponse\x12\x13\n\x0btemperature\x18\x01 \x01(\t28\n\x0bTestService\x12)\n\x08SendData\x12\x0c.DataRequest\x1a\r.DataResponse\"\x00\x42\x14\xaa\x02\x11TestServiceClientb\x06proto3')

_globals = globals()
_builder.BuildMessageAndEnumDescriptors(DESCRIPTOR, _globals)
_builder.BuildTopDescriptorsAndMessages(DESCRIPTOR, 'TestService_pb2', _globals)
if not _descriptor._USE_C_DESCRIPTORS:
  _globals['DESCRIPTOR']._loaded_options = None
  _globals['DESCRIPTOR']._serialized_options = b'\252\002\021TestServiceClient'
  _globals['_DATAREQUEST']._serialized_start=21
  _globals['_DATAREQUEST']._serialized_end=51
  _globals['_DATARESPONSE']._serialized_start=53
  _globals['_DATARESPONSE']._serialized_end=88
  _globals['_TESTSERVICE']._serialized_start=90
  _globals['_TESTSERVICE']._serialized_end=146
# @@protoc_insertion_point(module_scope)
