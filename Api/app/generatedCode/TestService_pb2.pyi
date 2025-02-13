from google.protobuf import descriptor as _descriptor
from google.protobuf import message as _message
from typing import ClassVar as _ClassVar, Optional as _Optional

DESCRIPTOR: _descriptor.FileDescriptor

class DataRequest(_message.Message):
    __slots__ = ("data_id",)
    DATA_ID_FIELD_NUMBER: _ClassVar[int]
    data_id: str
    def __init__(self, data_id: _Optional[str] = ...) -> None: ...

class DataResponse(_message.Message):
    __slots__ = ("temperature",)
    TEMPERATURE_FIELD_NUMBER: _ClassVar[int]
    temperature: str
    def __init__(self, temperature: _Optional[str] = ...) -> None: ...
