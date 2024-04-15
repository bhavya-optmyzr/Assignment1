MyModel = Backbone.Model.extend()

MyCollection = Backbone.Collection.extend
  model: MyModel
  initialize: (options) ->
    @latitude = options.latitude
    @longitude = options.longitude
  url: ->
    "https://api.openweathermap.org/data/2.5/weather?q=Delhi,in&APPID=cddbf0b42be47fe6061841ce93bf9ab6"

# Function to create a new card element with weather information
createCard = (location, temperature, description) ->
  cardDiv = document.createElement("div")
  cardDiv.className = "card"
  cardDiv.style = "width: 18rem; margin-bottom: 10px;"

  cardBody = document.createElement("div")
  cardBody.className = "card-body"

  title = document.createElement("h5")
  title.className = "card-title"
  title.innerText = "Location: "

  locationInput = document.createElement("input")
  locationInput.className = "form-control mb-2"
  locationInput.value = location

  temp = document.createElement("p")
  temp.className = "card-text"
  temp.innerText = "Temperature: #{temperature} °C"

  desc = document.createElement("p")
  desc.className = "card-text"
  desc.innerText = "Description: #{description}"

  buttonGroup = document.createElement("div")
  buttonGroup.className = "btn-group"

  updateBtn = document.createElement("button")
  updateBtn.className = "btn btn-primary mr-2"
  updateBtn.innerText = "Update"
  updateBtn.onclick = ->
    newLocation = locationInput.value
    if !newLocation
      alert("Please enter a location")
      return
    fetchWeatherAndUpdateCard newLocation, temperature, description, cardDiv

  deleteBtn = document.createElement("button")
  deleteBtn.className = "btn btn-danger"
  deleteBtn.innerText = "Delete"
  deleteBtn.onclick = ->
    console.log("Delete button clicked for location: #{location}")
    cardDiv.remove()

  buttonGroup.appendChild(updateBtn)
  buttonGroup.appendChild(deleteBtn)

  cardBody.appendChild(title)
  cardBody.appendChild(locationInput)
  cardBody.appendChild(temp)
  cardBody.appendChild(desc)
  cardBody.appendChild(buttonGroup)

  cardDiv.appendChild(cardBody)

  return cardDiv

# Function to fetch weather data for a location and update the card
fetchWeatherAndUpdateCard = (location, temperature, description, cardDiv) ->
  url = "https://api.openweathermap.org/data/2.5/weather?q=#{location}&APPID=cddbf0b42be47fe6061841ce93bf9ab6"

  fetch(url)
    .then((response) -> response.json())
    .then((data) ->
      cardTitle = cardDiv.querySelector(".card-title")
      tempText = cardDiv.querySelector(".card-text:nth-child(3)")
      descText = cardDiv.querySelector(".card-text:nth-child(4)")

      cardTitle.innerText = "Location: #{data.name}"
      tempText.innerText = "Temperature: #{(data.main.temp - 273).toFixed(2)} °C"
      descText.innerText = "Description: #{data.weather[0].description}"
    )
    .catch((error) ->
      console.error("Error fetching data:", error)
      alert("Error fetching data. See console for details.")
    )

MyView = Backbone.View.extend
  el: "#api"
  initialize: ->
    @render()
  render: ->
    @$el.html("Loading...")
    @fetchData()
  fetchData: ->
    location = document.getElementById("locationInput").value
    if !location
      alert("Please enter a location")
      return
    url = "https://api.openweathermap.org/data/2.5/weather?q=#{location}&APPID=cddbf0b42be47fe6061841ce93bf9ab6"

    fetch(url)
      .then((response) -> response.json())
      .then((data) ->
        newCard = createCard(data.name, (data.main.temp - 273).toFixed(2), data.weather[0].description)
        container = document.getElementById("cardsContainer")
        container.appendChild(newCard)
      )
      .catch((error) ->
        console.error("Error fetching data:", error)
        @$el.html("Error fetching data. See console for details.")
      )

myView = new MyView()

document.getElementById("fetchWeatherBtn").addEventListener("click", -> myView.fetchData())
