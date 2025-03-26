import requests
from frcm.datamodel.model import Location

"""
Fetches the latitude and longitude of a given city using the GPS Coordinates API.

Args:
    city (str): The name of the city to fetch coordinates for.

Returns:
    list: A list containing [latitude, longitude] if the request is successful.
    dict: An error message if the request fails or coordinates are not found.
"""
def getCoordinates(city: str):
    url = f"http://www.gps-coordinates.net/api/{city}"
    response = requests.get(url)

    if response.status_code == 200:
        data = response.json()

        if "latitude" in data and "longitude" in data:
            try:
                lat = float(data["latitude"])
                lon = float(data["longitude"])
                return Location(latitude=lat, longitude=lon)
            except ValueError:
                return {"error": "Invalid coordinate format"}
        else:
            return {"error": "Coordinates not found"}
    else:
        return {"error": f"Request failed with status code {response.status_code}"}
