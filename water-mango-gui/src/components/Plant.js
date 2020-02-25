import React, { useState, useEffect } from "react";
import Card from "@material-ui/core/Card";
import CardActions from "@material-ui/core/CardActions";
import CardContent from "@material-ui/core/CardContent";
import Button from "@material-ui/core/Button";
import Typography from "@material-ui/core/Typography";

import plant from "./plant.gif";

function Plant(props) {
  const [stateColor, setStateColor] = useState();
  const [canWater, setCanWater] = useState(false);
  const [canStopWatering, setCanStopWatering] = useState(false);
  const [actionName, setActionName] = useState("Water");

  // run this once when this component gets mounted
  useEffect(() => {
    // set the color of the state
    const color = getColorForState(props.state);
    setStateColor(color);

    let canWeWater = false;
    if (props.state === 0 || props.state === 3) {
      canWeWater = true;
    }

    setCanWater(canWeWater);
    setActionName("Water");

    let stopWatering = false;
    if (props.state === 1) {
      // we're watering
      stopWatering = true;
      setActionName("Stop");
    }
    setCanStopWatering(stopWatering);
  }, [props.state]);

  return (
    <Card className="Plant">
      <CardContent
        style={{
          display: "flex",
          justifyContent: "center",
          flexDirection: "column"
        }}
      >
        <Typography>{props.name}</Typography>
        <img style={{ height: "150px" }} src={plant} />
        <Typography>
          This is a lovely plant.. Don't water it after midnight...
        </Typography>
      </CardContent>
      <CardActions>
        <Button
          disabled={!canWater && !canStopWatering}
          size="small"
          onClick={() =>
            canStopWatering
              ? props.stopWateringCallback(props.id)
              : props.waterCallback(props.id)
          }
        >
          {actionName}
        </Button>
        <label
          style={{
            color: stateColor,
            verticalAlign: "center",
            padding: "0 0 5px 0"
          }}
        >
          Status: {getNameForState(props.state)}
        </label>
      </CardActions>
    </Card>
  );
}

function getNameForState(stateNumber) {
  // 0-> Idle,
  // 1 -> Watering
  // 2 -> Cooldown
  // 3 -> ThirstyAF

  switch (stateNumber) {
    case 0:
      return "Idle";
    case 1:
      return "Watering";
    case 2:
      return "Cooldown";
    case 3:
      return "Thirsty AF";
    default:
      return "Invalid";
  }
}

function getColorForState(stateNumber) {
  switch (stateNumber) {
    case 0:
      return "green";
    case 1:
      return "blue";
    case 2:
      return "grey";
    case 3:
      return "red";
    default:
      return "magenta";
  }
}

export default Plant;
