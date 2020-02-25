import React from "react";
import Plant from "./Plant";

function createPlants(plants, waterCallback, stopWateringCallback) {
  if (plants === undefined) {
    return <div></div>;
  }
  const plantComponents = plants.map(plant => (
    <Plant
      key={plant.id}
      id={plant.id}
      name={plant.name}
      state={plant.state}
      waterCallback={waterCallback}
      stopWateringCallback={stopWateringCallback}
    />
  ));
  return plantComponents;
}

function PlantContainer(props) {
  return (
    <div className="PlantContainer">
      {createPlants(
        props.plants,
        props.waterCallback,
        props.stopWateringCallback
      )}
    </div>
  );
}

export default PlantContainer;
