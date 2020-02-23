import React from "react";
import Card from "@material-ui/core/Card";
import CardActions from "@material-ui/core/CardActions";
import CardContent from "@material-ui/core/CardContent";
import Button from "@material-ui/core/Button";
import Typography from "@material-ui/core/Typography";

import plant from "./plant.gif";

function Plant(props) {
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
        <Button disabled={props.canWater} size="small">
          Water
        </Button>
        <label style={{ verticalAlign: "center" }}>Status: Idle</label>
      </CardActions>
    </Card>
  );
}

export default Plant;
