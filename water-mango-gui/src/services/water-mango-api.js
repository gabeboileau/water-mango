const axios = require("axios");

const instance = axios.create({
  baseURL: "http://localhost:5000/api/",
  timeout: 1000
});

async function getAllPlants() {
  const response = await instance.get("plant").catch(err => {
    console.warn(err);
    return [];
  });

  return response;
}

export async function waterPlant(plantId) {
  if (isNaN(plantId)) {
    return [];
  }

  if (plantId >= 0) {
    // it's valid - le-go
    const response = await instance.post("plant/water/" + plantId);
    return response;
  } else {
    return [];
  }
}

export async function stopWateringPlant(plantId) {
  if (isNaN(plantId)) {
    return [];
  }

  if (plantId >= 0) {
    const response = await instance.post("/plant/stopWatering/" + plantId);
    return response;
  } else {
    return [];
  }
}

export default getAllPlants;
