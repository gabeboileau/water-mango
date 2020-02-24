import React, { useState, useEffect } from "react";
import PlantContainer from "./PlantContainer";
import Footer from "./Footer";

import getAllPlants from "../services/water-mango-api";
import { waterPlant } from "../services/water-mango-api";

function MainPage() {
  const [plants, setPlants] = useState([]);
  const [plantContainer, setPlantContainer] = useState();

  // when the component mounts - we fetch the plants using the API
  useEffect(() => {
    const fetchData = async () => {
      const result = await getAllPlants();
      setPlants(result.data);
    };

    fetchData();
  }, []);

  return (
    <div className="MainPage">
      <img className="PlantHeader" src={"WaterMango-Logo.png"} />
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
      plants={plants}
    ></PlantContainer>
  );
}

function waterPlantCallback(plantId) {
  waterPlant(plantId);
}

export default MainPage;
