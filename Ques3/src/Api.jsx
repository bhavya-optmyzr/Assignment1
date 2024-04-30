const API_ID = "cddbf0b42be47fe6061841ce93bf9ab6";

export function fetchData(cardCity, setData) {
  let url = `https://localhost:7148/WeatherInfo/${cardCity}`;
  try {
    const response = fetch(url)
      .then((response) => {
        return response.json();
      })
      .then((data) => {
        setData({
          temp: (data.main.temp - 273.15).toFixed(2),
          city: data.name,
          feelsLike: (data.main.feels_like - 273.15).toFixed(2),
          desc: data.weather[0].description,
        });
      });
  } catch (err) {
    alert(err);
  }
}