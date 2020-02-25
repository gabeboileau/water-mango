import React, { useState, useEffect } from "react";
import PlantContainer from "./PlantContainer";
import Footer from "./Footer";

import getAllPlants from "../services/water-mango-api";
import { waterPlant, stopWateringPlant } from "../services/water-mango-api";

import { connect } from "../services/event-service";

function MainPage() {
  const [plants, setPlants] = useState([]);

  // when the component mounts - we fetch the plants using the API
  useEffect(() => {
    const fetchData = async () => {
      const result = await getAllPlants();
      if (result.data !== undefined) {
        setPlants(result.data);
      }
    };

    connect(async id => {
      const result = await getAllPlants();
      setPlants(result.data);
    });

    fetchData();
  }, []);

  return (
    <div className="MainPage">
      <img alt="ohboi" className="PlantHeader" src={"WaterMango-Logo.png"} />
      {createPlantContainer(plants)}
      <Footer />
    </div>
  );
}

function createPlantContainer(plants) {
  return (
    <PlantContainer
      className="PlantContainer"
      waterCallback={waterPlantCallback}
      stopWateringCallback={stopWateringCallback}
      plants={plants}
    ></PlantContainer>
  );
}

function waterPlantCallback(plantId) {
  waterPlant(plantId);
}

function stopWateringCallback(plantId) {
  console.log("Stop");
  stopWateringPlant(plantId);
}

export default MainPage;
