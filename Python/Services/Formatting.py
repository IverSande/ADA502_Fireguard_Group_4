

# color coding for the risk levels
def get_color(avg_ttf):
    if avg_ttf < 4.5:
        return "Red"
    elif avg_ttf < 5.5:
        return "Orange"
    elif avg_ttf < 6.5:
        return "Yellow"
    elif avg_ttf < 7.5:
        return "LightGreen"
    else:
        return "Green"