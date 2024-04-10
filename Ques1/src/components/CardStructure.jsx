import Button from "react-bootstrap/Button";
import Card from "react-bootstrap/Card";
import DayData from "./DayData";
import {useState} from 'react';

function CardStructure({ city , weatherMain , onClick}) {

  return (
    <Card>
      <Card.Header as="h5">{city}</Card.Header>
      <Card.Body>
        <Card.Title>Mainly : {weatherMain}</Card.Title>
        <Card.Text>
          <div id="weather-data">
            <DayData/>
            <DayData/>
            <DayData/>
            <DayData/>
            <DayData/>
            <DayData/>
            <DayData/>
          </div>
        </Card.Text>
        {/* <Button variant="primary" onClick={()=>onClick(input)}>Add Location</Button> */}
        <div id="buttons">
          <Button variant="primary" onClick={()=>onClick(input)}>Update</Button>
          <Button variant="primary" onClick={()=>onClick(input)}>Delete</Button>
        </div>
      </Card.Body>
    </Card>
  );
}

export default CardStructure;
