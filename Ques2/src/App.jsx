import Button from "react-bootstrap/Button";
import CardStructure from "./components/CardStructure";
import { useState } from "react";

export default function App() {
  const [city, setCity] = useState("London,uk");
  const [input, setInput] = useState("");
  const [cards, setCards] = useState([]);
  const [prevId, setPrevId] = useState(0);

  function handleClick() {
    setCity(input);
    const id = prevId + 1;
    setPrevId(id);
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
        <Button variant="success" onClick={() => handleClick()}>
          Add Location
        </Button>
      </div>

      <div id="cards">
        {cards.map((data, index) => {
          return (
            // earlier i was using key as index which was causing error I have changed it to data's index which has resolved the bug.
            <CardStructure
              key={data.id}
              city={data.city}
              identifier={data.id}
              onDelete={handleDelete}
            />
          );
        })}
      </div>
    </>
  );
}
