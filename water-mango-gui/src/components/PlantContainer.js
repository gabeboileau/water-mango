import React from "react";
import Plant from "./Plant";

function PlantContainer(props) {
  return (
    <div className="PlantContainer">
      <Plant name="Gabe" canWater="false"></Plant>
      <Plant name="Christine's Plant"></Plant>
    </div>
  );
}

export default PlantContainer;
