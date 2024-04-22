import Button from "react-bootstrap/Button";
import Card from "react-bootstrap/Card";
import { fetchData } from "../Api";
import { useState, useEffect } from "react";

function CardStructure({ city, onDelete, identifier }) {
  const [label, setLabel] = useState(true);
  const [inputCity, setInputCity] = useState("");
  const [cityName, setCityName] = useState(city);
  const [data, setData] = useState({
    temp: "",
    city: "",
    feelsLike: "",
    desc: "",
  });

  useEffect(() => {
    fetchData(cityName, setData);
  }, [cityName]);

  function onClickEdit() {
    setLabel(false);
  }

  function onClickSave() {
    setLabel(true);
    setCityName(inputCity);
  }

  return (
    <Card className="cardBody">
      {label ? (
        <div class="label-area">
          <Card.Header as="h5">{data.city}</Card.Header>
          <Button variant="primary" onClick={onClickEdit}>
            Edit
          </Button>
        </div>
      ) : (
        <div class="label-area">
          <input
            value={inputCity}
            required
            onChange={(event) => {
              setInputCity(event.target.value);
            }}
          />
          <Button variant="primary" onClick={onClickSave}>
            Save
          </Button>
        </div>
      )}

      <Card.Body>
        <Card.Title>Temperature : {data.temp} °C</Card.Title>
        <Card.Text>
          <div id="weather-data">
            Description : {data.desc}
            <br />
            Feels Like : {data.feelsLike} °C
          </div>
        </Card.Text>
        <div id="buttons">
          <Button variant="danger" onClick={() => onDelete(identifier)}>
            Delete
          </Button>
        </div>
      </Card.Body>
    </Card>
  );
}

export default CardStructure;
