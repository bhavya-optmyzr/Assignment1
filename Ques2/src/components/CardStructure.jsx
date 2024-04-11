import Button from "react-bootstrap/Button";
import Card from "react-bootstrap/Card";
import DayData from "./DayData";
import { useState } from "react";

function CardStructure({
  city,
  weatherMain,
  onClick,
  onDelete,
  identifier,
  temperature,
  feelLike,
}) {
  function handleDelete() {}

  return (
    <Card className="cardBody">
      <Card.Header as="h5">{city}</Card.Header>
      <hr></hr>
      <Card.Body className={weatherMain === "Clouds" ? "clouds" : "rainy"}>
        <Card.Title>Temperature : {temperature} °C</Card.Title>
        <Card.Text>
          <div id="weather-data">
            Description : {weatherMain}
            <br />
            Feels Like : {feelLike} °C
            {/* <DayData />
            <DayData />
            <DayData />
            <DayData />
            <DayData />
            <DayData />
            <DayData /> */}
          </div>
        </Card.Text>
        {/* <Button variant="primary" onClick={()=>onClick(input)}>Add Location</Button> */}
        <div id="buttons">
          <Button variant="warning" onClick={() => onClick(input)}>
            Update
          </Button>
          <Button variant="danger" onClick={() => onDelete(identifier)}>
            Delete
          </Button>
        </div>
      </Card.Body>
    </Card>
  );
}

export default CardStructure;
