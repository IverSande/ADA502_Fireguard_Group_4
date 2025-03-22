from fastapi import FastAPI
import datetime
import Coordinates
from Formatting import get_color 
import pandas as pd

from frcm.frcapi import METFireRiskAPI

# constants
days = 2


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
    coordinates = Coordinates.getCoordinates(Location)
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
        coordinates = Coordinates.getCoordinates(location)
        # decide how far back to go for observed weather data
        timeDelta = datetime.timedelta(days)
        predictions = frc.compute_now(coordinates, timeDelta)
        averages.append(float(getAverageTTF(predictions)))
    
    return averages




# what is needed for db?
# user needs list of saved locations