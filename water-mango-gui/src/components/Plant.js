import React from "react";
import Card from "@material-ui/core/Card";
import CardActions from "@material-ui/core/CardActions";
import CardContent from "@material-ui/core/CardContent";
import Button from "@material-ui/core/Button";
import Typography from "@material-ui/core/Typography";

import plant from "./plant.gif";

function Plant(props) {
  console.log(props);
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
        <Typography>This is a lovely plant.. it's just so nice.</Typography>
      </CardContent>
      <CardActions>
        <Button
          disabled={props.canWater}
          size="small"
          onClick={() => props.waterCallback(props.id)}
        >
          Water
        </Button>
        <label style={{ verticalAlign: "center" }}>
          Status: {getNameForState(props.state)}
        </label>
      </CardActions>
    </Card>
  );
}

function getNameForState(stateNumber) {
  console.log(stateNumber);
  // 1-> Idle,
  // 2 -> Watering
  // 3 -> Cooldown
  // 4 -> ThirstyAF

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

export default Plant;
