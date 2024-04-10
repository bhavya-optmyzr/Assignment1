import Button from "react-bootstrap/Button";
import Card from "react-bootstrap/Card";
import DayData from "./DayData";

function CardStructure({ city , weatherMain}) {
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
        <Button variant="primary">Go somewhere</Button>
      </Card.Body>
    </Card>
  );
}

export default CardStructure;
