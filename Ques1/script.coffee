# Model for location
class Location extends Backbone.Model
  defaults:
    name: ""
    temperature: 0
    unit: "Celsius"

# Collection of locations
class LocationsCollection extends Backbone.Collection
  model: Location

# View for displaying temperature info
class TemperatureView extends Backbone.View
  initialize: ->
    console.log("TemperatureView initialized")
    @collection = new LocationsCollection
    @render()
    @listenTo(@collection, "add", @fetchTemperature)
    @listenTo(@collection, "remove", @render)
    @listenTo(@collection, "change", @render)

fetchTemperature: (location) ->
  console.log("Location type:", typeof location)
  if location instanceof Location
    url = "https://api.openweathermap.org/data/2.5/weather?q=#{location.get('name')}&APPID=cddbf0b42be47fe6061841ce93bf9ab6"
    $.getJSON(url, (data) =>
      if data.main?
        temperature = data.main.temp
        # Convert temperature from Kelvin to Celsius
        temperature = temperature - 273.15
        location.set('temperature', temperature.toFixed(2))
      else
        console.error("Error: No temperature data returned for location", location.get('name'))
      # After setting temperature, re-render the view
      @render()
    )
  else
    console.error("Error: Invalid location model passed to fetchTemperature")

  events:
    "click .delete": "deleteLocation"

  deleteLocation: (event) ->
    locationName = $(event.target).siblings("p").first().text().split(":")[1].trim()
    location = @collection.findWhere(name: locationName)
    @collection.remove(location)

# View for adding new location
class AddLocationView extends Backbone.View
  initialize: ->
    @collection = new LocationsCollection() # Instantiate the collection
    @render()

  render: ->
    $form = $('<form>')
    $form.append("<input type='text' id='name' placeholder='Location Name'>")
    $form.append("<button id='add'>Add Location</button>")
    @$el.append($form)

  events:
    "click #add": "addLocation"

  addLocation: (event) ->
    name = $('#name').val()
    location = new Location(name: name) # Instantiate the Location model
    if name
      @collection.add(location) # Add the Location model to the collection
    else
      console.error("Error: Please provide a location name")

# Main App View
class AppView extends Backbone.View
  initialize: ->
    console.log("AppView initialized")
    @render()

  render: ->
    @temperatureView = new TemperatureView(el: $("#temperature-info"))
    @addLocationView = new AddLocationView(el: $("#add-location"))
