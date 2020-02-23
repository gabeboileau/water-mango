import React, { useState, useEffect } from "react";
import PlantContainer from "./PlantContainer";

import getAllPlants from "../services/water-mango-api";

function MainPage() {
  const [plants, setPlants] = useState([]);
  const [plantContainer, setPlantContainer] = useState();

  useEffect(() => {
    getAllPlants().then(response => {
      console.log(response.data);
      setPlants(response.data);
    });
  }, []);

  return (
    <div className="MainPage">
      <img className="PlantHeader" src={"WaterMango-Logo.png"} />
      {createPlantContainer(plants)}
    </div>
  );
}

function createPlantContainer(plants) {
  return (
    <PlantContainer className="PlantContainer" plants={plants}></PlantContainer>
  );
}

export default MainPage;
