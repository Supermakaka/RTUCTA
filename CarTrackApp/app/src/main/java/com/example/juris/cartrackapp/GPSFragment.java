package com.example.juris.cartrackapp;

import android.app.Activity;
import android.app.Fragment;
import android.content.Context;
import android.content.Intent;
import android.location.Criteria;
import android.location.Location;
import android.location.LocationListener;
import android.location.LocationManager;
import android.net.Uri;
import android.os.Bundle;
import android.os.Handler;
import android.provider.Settings;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;
import android.widget.Toast;

import org.apache.http.NameValuePair;
import org.apache.http.message.BasicNameValuePair;
import org.json.JSONObject;

import java.util.ArrayList;
import java.util.Timer;
import java.util.TimerTask;


public class GPSFragment extends Fragment implements LocationListener {
    // TODO: Rename parameter arguments, choose names that match
    // the fragment initialization parameters, e.g. ARG_ITEM_NUMBER
    private static final String ARG_LONGITUDE = "longitude";
    private static final String ARG_LATITUDE = "latitude";
    private static final String ARG_ACCURACY = "accuracy";
    private static final String ARG_ALTITUDE = "altitude";
    private static final String ARG_TIME = "time";

    // TODO: Rename and change types of parameters
    private String longitude;
    private String latitude;
    private String accuracy;
    private String altitude;
    private String time;
    private String speed;
    private String provider;
    private TextView longitudeTextView;
    private TextView latitudeTextView;
    private TextView accuracyTextView;
    private TextView altitudeTextView;
    private TextView timeTextView;
    private DateHelperClass dateHelperClass;

    private HttpResponseHadlerActivity httpHandler;

    private LocationManager locationManager;

    private OnFragmentInteractionListener mListener;

    public GPSFragment() {

    }

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        View view = inflater.inflate(R.layout.fragment_gps, container, false);

        longitudeTextView = (TextView) view.findViewById(R.id.longitude);
        latitudeTextView = (TextView) view.findViewById(R.id.latitude);
        accuracyTextView = (TextView) view.findViewById(R.id.accuracy);
        altitudeTextView = (TextView) view.findViewById(R.id.altitude);
        timeTextView = (TextView) view.findViewById(R.id.time);

        locationManager = (LocationManager) getActivity().getSystemService(Context.LOCATION_SERVICE);

        dateHelperClass = new DateHelperClass();
        Criteria criteria = new Criteria();
        criteria.setAccuracy(Criteria.ACCURACY_FINE);

        httpHandler = new HttpResponseHadlerActivity();

        provider = locationManager.getBestProvider(criteria, true);
        locationManager.requestLocationUpdates(LocationManager.GPS_PROVIDER, 5, 5, this);
        Location location = locationManager.getLastKnownLocation(provider);

        boolean enabled = locationManager.isProviderEnabled(LocationManager.GPS_PROVIDER);

        if (!enabled) {
            Intent intent = new Intent(Settings.ACTION_LOCATION_SOURCE_SETTINGS);
            startActivity(intent);
        }

        if (location != null) {
            System.out.println("Provider " + provider + " has been selected.");
            onLocationChanged(location);
        } else {
            latitudeTextView.setText("Location not available");
            longitudeTextView.setText("Location not available");
        }

        return view;
    }

    // TODO: Rename method, update argument and hook method into UI event
    public void onButtonPressed(Uri uri) {
        if (mListener != null) {
            mListener.onFragmentInteraction(uri);
        }
    }

    @Override
    public void onAttach(Activity activity) {
        super.onAttach(activity);
        try {
            mListener = (OnFragmentInteractionListener) activity;
        } catch (ClassCastException e) {
            throw new ClassCastException(activity.toString()
                    + " must implement OnFragmentInteractionListener");
        }
    }

    @Override
    public void onDetach() {
        super.onDetach();
        mListener = null;
    }

    @Override
    public void onLocationChanged(Location location) {

        longitude = String.valueOf(location.getLongitude());
        latitude = String.valueOf(location.getLatitude());
        accuracy = String.valueOf(location.getAccuracy());
        altitude = String.valueOf(location.getAltitude());
        time = dateHelperClass.GetDate(location.getTime());
        speed = String.valueOf(location.getSpeed());

        longitudeTextView.setText(longitude);
        latitudeTextView.setText(latitude);
        accuracyTextView.setText(accuracy);
        altitudeTextView.setText(altitude);
        timeTextView.setText(time);

        JSONObject jsonObject = new JSONObject();
        try {
            jsonObject.accumulate("Longitude", longitude);
            jsonObject.accumulate("Latitude", latitude);
            jsonObject.accumulate("Time", time);
            jsonObject.accumulate("Altitude", altitude);
            jsonObject.accumulate("Accuracy", accuracy);
            jsonObject.accumulate("CarId", "1");
            jsonObject.accumulate("Speed", speed);
        }catch (Exception e)
        {
            e.printStackTrace();
        }

        httpHandler.makePostRequest("http://supermakaka-001-site1.smarterasp.net/Location/RetriveDataFromAndroid", jsonObject.toString());
    }

    @Override
    public void onStatusChanged(String provider, int status, Bundle extras) {

    }

    @Override
    public void onProviderEnabled(String provider) {
        Toast.makeText(getActivity(), "Enabled new provider " + provider,
                Toast.LENGTH_SHORT).show();
    }

    @Override
    public void onProviderDisabled(String provider) {
        Toast.makeText(getActivity(), "Disabled provider " + provider,
                Toast.LENGTH_SHORT).show();
    }

    /**
     * This interface must be implemented by activities that contain this
     * fragment to allow an interaction in this fragment to be communicated
     * to the activity and potentially other fragments contained in that
     * activity.
     * <p/>
     * See the Android Training lesson <a href=
     * "http://developer.android.com/training/basics/fragments/communicating.html"
     * >Communicating with Other Fragments</a> for more information.
     */
    public interface OnFragmentInteractionListener {
        // TODO: Update argument type and name
        public void onFragmentInteraction(Uri uri);
    }

}
