import React from "react";
import PlantContainer from "./PlantContainer";

export default class MainPage extends React.Component {
  render() {
    return (
      <div className="MainPage">
        <img className="PlantHeader" src={"WaterMango-Logo.png"} />
        <PlantContainer className="PlantContainer"></PlantContainer>
      </div>
    );
  }
}
