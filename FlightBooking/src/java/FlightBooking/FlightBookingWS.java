/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package FlightBooking;

import javax.jws.WebService;
import javax.jws.WebMethod;
import javax.jws.WebParam;
import java.util.List;
import java.io.File;
import java.util.Iterator;
import java.util.GregorianCalendar;
import java.util.logging.Level;
import java.util.logging.Logger;
import javax.xml.datatype.DatatypeConfigurationException;
import javax.xml.datatype.XMLGregorianCalendar;
import javax.xml.datatype.DatatypeFactory;
/**
 *
 * @author N0561281
 */
@WebService(serviceName = "FlightBookingWS")
public class FlightBookingWS {

    /**
     * This is a sample web service operation
     * @return 
     */
    @WebMethod(operationName = "setup")
    public Flights setup() {
        Flights flights = new Flights();
        List<Flight> flightList = flights.getFlight();
        Flight flight;
        Fare fare;
        GregorianCalendar c;
        XMLGregorianCalendar flightDate;
        Address airport;

        flight = new FlightBooking.Flight();
        flight.setId(1);
        flight.setAirline("Virgin Atlantic");
        flight.setWebsite("virginatlantic.com");
        airport = new Address();
        airport.setAirport("Manchester Airport");
        airport.setCity("Manchester");
        airport.setCountry("United Kingdom");
        flight.setDestinationCity(airport);
        airport = new Address();
        airport.setAirport("London Luton Airport");
        airport.setCity("London");
        airport.setCountry("United Kingdom");
        flight.setOriginCity(airport);
        flight.setNoOfSeats(20000);
        flight.setNoOfConnections(0);
        c = new GregorianCalendar(2017, 05, 19);
        try {
            flightDate = DatatypeFactory.newInstance().newXMLGregorianCalendar(c);
            flight.setDate(flightDate);
        } catch (DatatypeConfigurationException ex) {
            Logger.getLogger(FlightBookingWS.class.getName()).log(Level.SEVERE, null, ex);
        }
        fare = new FlightBooking.Fare();
        fare.setCurrency("GBP");
        fare.setValue(100);
        flight.setFare(fare);
        flightList.add(flight);

        flight = new FlightBooking.Flight();
        flight.setId(5);
        flight.setAirline("EasyJet");
        flight.setWebsite("easyjet.com");
        airport = new Address();
        airport.setAirport("East Midlands Airport");
        airport.setCity("Castle Donnington");
        airport.setCountry("United Kingdom");
        flight.setDestinationCity(airport);
        airport = new Address();
        airport.setAirport("Geneva International Airport");
        airport.setCity("Geneva");
        airport.setCountry("Switzerland");
        flight.setOriginCity(airport);
        flight.setNoOfSeats(110);
        flight.setNoOfConnections(0);
        c = new GregorianCalendar(2017, 05, 20);
        try {
            flightDate = DatatypeFactory.newInstance().newXMLGregorianCalendar(c);
            flight.setDate(flightDate);
        } catch (DatatypeConfigurationException ex) {
            Logger.getLogger(FlightBookingWS.class.getName()).log(Level.SEVERE, null, ex);
        }
        fare = new FlightBooking.Fare();
        fare.setCurrency("GBP");
        fare.setValue(130);
        flight.setFare(fare);
        flightList.add(flight);

        flight = new FlightBooking.Flight();
        flight.setId(2);
        flight.setAirline("EasyJet");
        flight.setWebsite("easyjet.com");
        airport = new Address();
        airport.setAirport("East Midlands Airport");
        airport.setCity("Castle Donnington");
        airport.setCountry("United Kingdom");
        flight.setDestinationCity(airport);
        airport = new Address();
        airport.setAirport("Geneva International Airport");
        airport.setCity("Geneva");
        airport.setCountry("Switzerland");
        flight.setOriginCity(airport);
        flight.setNoOfSeats(610);
        flight.setNoOfConnections(1);
        c = new GregorianCalendar(2017, 05, 20);
        try {
            flightDate = DatatypeFactory.newInstance().newXMLGregorianCalendar(c);
            flight.setDate(flightDate);
        } catch (DatatypeConfigurationException ex) {
            Logger.getLogger(FlightBookingWS.class.getName()).log(Level.SEVERE, null, ex);
        }
        fare = new FlightBooking.Fare();
        fare.setCurrency("GBP");
        fare.setValue(330);
        flight.setFare(fare);
        flightList.add(flight);
 
        flight = new FlightBooking.Flight();
        flight.setId(3);
        flight.setAirline("American Airlines");
        flight.setWebsite("aa.com");
        airport = new Address();
        airport.setAirport("Heathrow Airport");
        airport.setCity("London");
        airport.setCountry("United Kindom");
        flight.setDestinationCity(airport);
        airport = new Address();
        airport.setAirport("JFK Airport");
        airport.setCity("New York");
        airport.setCountry("United States");
        flight.setOriginCity(airport);
        flight.setNoOfSeats(20000);
        flight.setNoOfConnections(0);
        c = new GregorianCalendar(2017, 05, 21);
        try {
            flightDate = DatatypeFactory.newInstance().newXMLGregorianCalendar(c);
            flight.setDate(flightDate);
        } catch (DatatypeConfigurationException ex) {
            Logger.getLogger(FlightBookingWS.class.getName()).log(Level.SEVERE, null, ex);
        }
        fare = new FlightBooking.Fare();
        fare.setCurrency("GBP");
        fare.setValue(1500);
        flight.setFare(fare);
        flightList.add(flight);

        try {            
            javax.xml.bind.JAXBContext jaxbCtx = javax.xml.bind.JAXBContext.newInstance(flights.getClass().getPackage().getName());
            javax.xml.bind.Marshaller marshaller = jaxbCtx.createMarshaller();
            marshaller.setProperty(javax.xml.bind.Marshaller.JAXB_ENCODING, "UTF-8"); //NOI18N
            marshaller.setProperty(javax.xml.bind.Marshaller.JAXB_FORMATTED_OUTPUT, Boolean.TRUE);
            
            File flightStore = new File("Flights.xml");
            marshaller.marshal(flights, flightStore);
        } catch (javax.xml.bind.JAXBException ex) {
            // XXXTODO Handle exception
            java.util.logging.Logger.getLogger("global").log(java.util.logging.Level.SEVERE, null, ex); //NOI18N
        }
        return flights;
    }

    /**
     * Web service operation
     * @return 
     */
    @WebMethod(operationName = "getAvailableSeats")
    public List<Flight> getAvailableSeats() {
        Flights flights = this.unmarshalFlights();
        List<Flight> flightList = flights.getFlight();
        Flights availableFlights = new Flights();
        List<Flight> availableFlightsList = availableFlights.getFlight();
        Iterator it = flightList.iterator();
        Flight nextFlight;
        while(it.hasNext()) {
            nextFlight = (Flight) it.next();
            if(nextFlight.getNoOfSeats() > 0) {
                availableFlightsList.add(nextFlight);
            }
        }
        return availableFlightsList;
    }

    /**
     * Web service operation
     * @param OriginCity
     * @param DestinationCity
     * @param Airline
     * @param DirectFlight
     * @return 
     */
    @WebMethod(operationName = "searchFlight")
    public List<Flight> searchFlight(@WebParam(name = "OriginCity") String OriginCity, 
            @WebParam(name = "DestinationCity") String DestinationCity, 
            @WebParam(name = "Airline") String Airline,
            @WebParam(name = "DirectFlight") Boolean DirectFlight
            //@WebParam(name = "FlightDate") String FlightDate
    ) {
        Flights flights = unmarshalFlights();
        List<Flight> flightList = flights.getFlight();
        Flights searchResults = new Flights();
        List<Flight> searchList = searchResults.getFlight();
        flightList.forEach((f) -> {
            Boolean match = false;
            if (!"".equals(OriginCity))
                match = f.originCity.city.equals(OriginCity);
            if (!"".equals(DestinationCity) && match == true)
                match = f.destinationCity.city.equals(DestinationCity);
            if(!"".equals(Airline) && match == true)
                match = f.airline.equals(Airline);
            if(DirectFlight && match == true)
                match = f.noOfConnections == 0;
            if (match) {
                searchList.add(f);
            }
        });
        return searchList;
    }

    /**
     * Web service operation
     * @param id
     * @param seats
     * @return boolean
     *  true = booking made, false = id didn't match any flights
     */
    @WebMethod(operationName = "bookFlight")
    public int bookFlight(@WebParam(name = "id") int id, @WebParam(name = "seats") int seats) {
        Flights flights = this.unmarshalFlights();
        List<Flight> flightList = flights.getFlight();
        for(Flight f : flightList) {
            if(f.getId() == id) {
                if(f.getNoOfSeats() < seats)
                {
                    f.setNoOfSeats(0);
                } else {
                    f.setNoOfSeats(f.getNoOfSeats() - seats);
                }
                return f.getNoOfSeats();
            }
        }
        return 0;
    }
    
    private Flights unmarshalFlights() {
        Flights flights = new Flights();
        try {
            javax.xml.bind.JAXBContext jaxbCtx = javax.xml.bind.JAXBContext.newInstance(flights.getClass().getPackage().getName());
            javax.xml.bind.Unmarshaller unmarshaller = jaxbCtx.createUnmarshaller();
            flights = (Flights) unmarshaller.unmarshal(new java.io.File("Flights.xml")); //NOI18N
        } catch (javax.xml.bind.JAXBException ex) {
            // XXXTODO Handle exception
            java.util.logging.Logger.getLogger("global").log(java.util.logging.Level.SEVERE, null, ex); //NOI18N
        }
        return flights;
    }
    
    private Boolean empty(String var)
    {
        return var == null || var.equals("");
    }   
    
}
