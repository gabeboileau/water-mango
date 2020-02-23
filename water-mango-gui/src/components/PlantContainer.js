import React from "react";
import Plant from "./Plant";

function createPlants(plants) {
  if (plants === undefined) {
    return <div></div>;
  }
  const plantComponents = plants.map(plant => (
    <Plant key={plant.id} name={plant.name} />
  ));
  return plantComponents;
}

function PlantContainer(props) {
  console.log(props);
  return <div className="PlantContainer">{createPlants(props.plants)}</div>;
}

export default PlantContainer;
