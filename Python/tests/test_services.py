# Python/tests/test_services.py

import pytest
from fastapi.testclient import TestClient
from unittest.mock import patch, MagicMock

# Import the FastAPI app from Services.main
from Services.main import app

# Import helper for color assertions
from Services.Formatting import get_color

# Import gRPC service and protobuf definitions
from grpc_service.service_impl import FireRiskGRPCService
import grpc_service.TestService_pb2 as pb2

client = TestClient(app)

# --- FastAPI endpoint tests ---

def test_read_root():
    resp = client.get("/")
    assert resp.status_code == 200
    assert resp.json() == {"message": "Welcome to the FireRisk API!"}

@patch("Services.main.getCoordinates")
def test_coordinates_endpoint(mock_get_coords):
    mock_get_coords.return_value = ["12.34", "56.78"]
    resp = client.get("/coordinates/TestCity")
    assert resp.status_code == 200
    assert resp.json() == {
        "location": "TestCity",
        "coordinates": ["12.34", "56.78"]
    }

@patch("Services.main.getCoordinates")
@patch("Services.main.METFireRiskAPI")
def test_average_ttf_endpoint(mock_api_cls, mock_get_coords):
    # Arrange: stub out coords + API
    mock_get_coords.return_value = ("lat","lon")
    dummy = MagicMock()
    # simulate two risks with ttf=2 and ttf=4
    dummy.compute_now.return_value = MagicMock(
        firerisks=[type("R", (), {"ttf":2})(), type("R", (), {"ttf":4})()]
    )
    mock_api_cls.return_value = dummy

    # Act
    resp = client.get("/average-ttf/MyLoc")
    assert resp.status_code == 200

    body = resp.json()
    assert body["location"] == "MyLoc"
    assert body["average_ttf"] == 3.0
    assert body["color"] == get_color(3.0)

# --- gRPC service test ---

def test_grpc_SendData():
    svc = FireRiskGRPCService()
    req = pb2.DataRequest(data_id="test123")
    resp = svc.SendData(req, None)
    assert hasattr(resp, "temperature")
    assert isinstance(resp.temperature, str)
    assert resp.temperature == "22°C"
