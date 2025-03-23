from fastapi import FastAPI, HTTPException
import datetime
from Coordinates import getCoordinates
from Formatting import get_color 
import pandas as pd
from frcm.frcapi import METFireRiskAPI
import traceback

app = FastAPI()

# constants
days = 2

# root endpoint, test by running in terminal python directory: uvicorn main:app --reload
@app.get("/")
def read_root():
    return {"message": "Welcome to the FireRisk API!"}

# get the average time to flashover, the higher the number the lower the risk
def getAverageTTF(prediction):
    # Extract all ttf values into a list
    ttf_values = [risk.ttf for risk in prediction.firerisks]
    # Create a pandas Series and compute the mean
    avg = pd.Series(ttf_values).mean()
    return round(avg, 2)



    
# - a function that takes a location(coordinates) as parameter, retrieves risk from FRCM, then returns the risk
def getFireRiskSingleLoc(Location):
    frc = METFireRiskAPI()
    
    # get coordinates for location by using name
    coordinates = getCoordinates(Location)
    # decide how far back to go for observed weather data
    timeDelta = datetime.timedelta(days)

    predictions = frc.compute_now(coordinates, timeDelta)
    
    return getAverageTTF(predictions)


# - a function that takes a list of locations and returns the average fire risk for all given locations
def getFireRiskMultipleLoc(locations):
    frc = METFireRiskAPI()

    averages = []
    for location in locations:
        # get coordinates for location by using name
        coordinates = getCoordinates(location)
        # decide how far back to go for observed weather data
        timeDelta = datetime.timedelta(days)
        predictions = frc.compute_now(coordinates, timeDelta)
        averages.append(float(getAverageTTF(predictions)))
    
    return averages




# what is needed for db?
# user needs list of saved locations




# API endpoints

# --- API Endpoints ---


@app.get("/coordinates/{location}")
def coordinates_endpoint(location: str):
    """
    Returns the coordinates for a given location.
    """
    try:
        coords = getCoordinates(location)
        return {"location": location, "coordinates": coords}
    except Exception as e:
        raise HTTPException(status_code=500, detail=str(e))


@app.get("/average-ttf/{location}")
def average_ttf_endpoint(location: str):
    """
    Returns the average TTF and corresponding color code for a single location.
    """
    try:
        avg_ttf = getFireRiskSingleLoc(location)
        color = get_color(avg_ttf)
        return {"location": location, "average_ttf": avg_ttf, "color": color}
    except Exception as e:
        raise HTTPException(status_code=500, detail=str(e))




@app.get("/multiple-locations")
def multiple_locations_endpoint(locations: str):
    try:
        # Split the locations by comma and strip extra spaces
        loc_list = [loc.strip() for loc in locations.split(",") if loc.strip()]
        averages = getFireRiskMultipleLoc(loc_list)
        results = []
        for loc, avg in zip(loc_list, averages):
            results.append({
                "location": loc,
                "average_ttf": avg,
                "color": get_color(avg)
            })
        return {"results": results}
    except Exception as e:
        # Capture the full traceback for detailed error info
        tb = traceback.format_exc()
        # Optionally log tb here
        raise HTTPException(status_code=500, detail=tb)