const axios = require("axios");

const instance = axios.create({
  baseURL: "https://localhost:5001/api/",
  timeout: 1000
});

async function getAllPlants() {
  const response = await instance.get("plant").catch(err => console.warn(err));
  return response;
}

export async function waterPlant(plantId) {
  if (plantId !== undefined && plantId >= 0) {
    // it's valid - le-go
    const response = await instance.post("plant/water/" + plantId);
    return response;
  } else {
    return [];
  }
}

export default getAllPlants;
