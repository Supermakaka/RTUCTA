package com.example.juris.cartrackapp;

import android.app.Activity;
import android.os.AsyncTask;
import android.os.Bundle;
import android.os.StrictMode;
import android.util.Log;

import org.apache.http.HttpResponse;
import org.apache.http.NameValuePair;
import org.apache.http.client.ClientProtocolException;
import org.apache.http.client.HttpClient;
import org.apache.http.client.entity.UrlEncodedFormEntity;
import org.apache.http.client.methods.HttpPost;
import org.apache.http.impl.client.DefaultHttpClient;
import org.apache.http.protocol.HTTP;

import java.io.IOException;
import java.io.UnsupportedEncodingException;
import java.util.List;

public class HttpResponseHadlerActivity {

    public void makePostRequest(String url, List<NameValuePair> data){

        StrictMode.ThreadPolicy policy = new StrictMode.ThreadPolicy.Builder().permitAll().build();

        StrictMode.setThreadPolicy(policy);

        HttpClient httpClient = new DefaultHttpClient();

        Log.e("INFOFORME_DATA", data.toString());

        HttpPost httpPost = new HttpPost(url);

        //Encoding POST data
        try {
            httpPost.setHeader(HTTP.CONTENT_TYPE,
                    "application/json");
            httpPost.setEntity(new UrlEncodedFormEntity(data, "UTF-8"));
        } catch (UnsupportedEncodingException e) {
            // log exception
            e.printStackTrace();
        }

        //making POST request.
        try {
            HttpResponse response = httpClient.execute(httpPost);
            // write response to log
            Log.d("INFOFORME_HTTP_RESPONSE", response.getStatusLine().toString());
        } catch (ClientProtocolException e) {
            // Log exception
            e.printStackTrace();
        } catch (IOException e) {
            // Log exception
            e.printStackTrace();
        }
    }
}
