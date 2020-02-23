const axios = require("axios");

const instance = axios.create({
  baseURL: "https://localhost:5001/api/",
  timeout: 1000
});

function getAllPlants() {
  const response = instance.get("plant");
  return response;
}

export default getAllPlants;
