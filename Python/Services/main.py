# Python/main.py
import threading
import uvicorn

from fastapi import FastAPI, HTTPException
import datetime, traceback
import pandas as pd

# Import your business logic (adjust paths as needed)
from Services.Coordinates import getCoordinates
from Services.Formatting import get_color 
from frcm.frcapi import METFireRiskAPI

app = FastAPI()

# --- FastAPI Endpoints ---
@app.get("/")
def read_root():
    return {"message": "Welcome to the FireRisk API!"}

@app.get("/coordinates/{location}")
def coordinates_endpoint(location: str):
    try:
        coords = getCoordinates(location)
        return {"location": location, "coordinates": coords}
    except Exception as e:
        raise HTTPException(status_code=500, detail=str(e))

@app.get("/average-ttf/{location}")
def average_ttf_endpoint(location: str):
    try:
        frc = METFireRiskAPI()
        coordinates = getCoordinates(location)
        timeDelta = datetime.timedelta(days=2)
        predictions = frc.compute_now(coordinates, timeDelta)
        avg_ttf = round(pd.Series([risk.ttf for risk in predictions.firerisks]).mean(), 2)
        return {"location": location, "average_ttf": avg_ttf, "color": get_color(avg_ttf)}
    except Exception as e:
        raise HTTPException(status_code=500, detail=str(e))

# ... add additional endpoints as needed ...

# --- Start the gRPC Server in a Separate Thread ---
def start_grpc():
    from grpc import service_impl  # Import our gRPC service implementation
    service_impl.serve_grpc()

if __name__ == "__main__":
    # Start gRPC service concurrently
    grpc_thread = threading.Thread(target=start_grpc, daemon=True)
    grpc_thread.start()
    
    # Run FastAPI via uvicorn on port 8000
    uvicorn.run(app, host="0.0.0.0", port=8000, reload=True)
