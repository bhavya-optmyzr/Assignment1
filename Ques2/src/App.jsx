import Button from "react-bootstrap/Button";
import CardStructure from "./components/CardStructure";
import { useContext, useState } from "react";
import { Context } from "./Api";

export const thisCity = "London,uk";

export default function App() {
  const { city } = useContext(Context);
  const { weatherMain } = useContext(Context);
  const { temp } = useContext(Context);
  const { feelsLike } = useContext(Context);

  const [input, setInput] = useState("");
  const [id, setId] = useState([]);

  const [cards, setCards] = useState([{ id: null, city: null }]);

  function handleClick(input) {
    const id = Math.random();
    setCards((prev) => {
      return [...prev, { id: id, city: input }];
    });
  }

  function handleDelete(identifier) {
    setCards((prev) => prev.filter((data) => data.id !== identifier));
  }

  return (
    <>
      {/* <h1>Weather App</h1> */}
      <div id="header">
        <input
          value={input}
          required
          onChange={(event) => {
            setInput(event.target.value);
          }}
        />
        <Button variant="success" onClick={() => handleClick(input)}>
          Add Location
        </Button>
      </div>

      <div id="cards">
        {cards.map((data, index) => {
          return (
            <CardStructure
              key={index}
              city={data.city}
              identifier={data.id}
              weatherMain={weatherMain}
              temperature={temp}
              feelLike={feelsLike}
              onClick={handleClick}
              onDelete={handleDelete}
            />
          );
        })}
      </div>
    </>
  );
}
