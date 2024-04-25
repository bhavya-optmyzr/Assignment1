const API_ID = "cddbf0b42be47fe6061841ce93bf9ab6";

export async function fetchData(cardCity, setData) {
  let url = `https://localhost:7148/WeatherInfo/${cardCity}`;
  try {
    const response = await fetch(url);
    if (!response.ok) {
      throw "This location is not defined...Try another...";
    }

    const data = await response.json();
    console.log(data);
    setData({
      temp: (data.main.temp).toFixed(2),
      city: data.name,
      feelsLike: (data.main.feels_like).toFixed(2),
      desc: data.weather[0].description,
    });
  } catch (err) {
    alert(err);
  }
}
