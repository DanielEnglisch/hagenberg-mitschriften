# CurrenciesApi

All URIs are relative to *http://localhost:56114/*

Method | HTTP request | Description
------------- | ------------- | -------------
[**currenciesGetAll**](CurrenciesApi.md#currenciesGetAll) | **GET** /api/currencies | 
[**currenciesGetBySymbol**](CurrenciesApi.md#currenciesGetBySymbol) | **GET** /api/currencies/{symbol} | 
[**currenciesInsert**](CurrenciesApi.md#currenciesInsert) | **POST** /api/currencies | 
[**currenciesRateOfExchange**](CurrenciesApi.md#currenciesRateOfExchange) | **GET** /api/currencies/{source}/rates/{target} | 
[**currenciesUpdate**](CurrenciesApi.md#currenciesUpdate) | **PUT** /api/currencies | 

<a name="currenciesGetAll"></a>
# **currenciesGetAll**
> List&lt;CurrencyData&gt; currenciesGetAll()



### Example
```java
// Import classes:
//import io.swagger.client.ApiException;
//import io.swagger.client.api.CurrenciesApi;


CurrenciesApi apiInstance = new CurrenciesApi();
try {
    List<CurrencyData> result = apiInstance.currenciesGetAll();
    System.out.println(result);
} catch (ApiException e) {
    System.err.println("Exception when calling CurrenciesApi#currenciesGetAll");
    e.printStackTrace();
}
```

### Parameters
This endpoint does not need any parameter.

### Return type

[**List&lt;CurrencyData&gt;**](CurrencyData.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

<a name="currenciesGetBySymbol"></a>
# **currenciesGetBySymbol**
> CurrencyData currenciesGetBySymbol(symbol)



### Example
```java
// Import classes:
//import io.swagger.client.ApiException;
//import io.swagger.client.api.CurrenciesApi;


CurrenciesApi apiInstance = new CurrenciesApi();
String symbol = "symbol_example"; // String | 
try {
    CurrencyData result = apiInstance.currenciesGetBySymbol(symbol);
    System.out.println(result);
} catch (ApiException e) {
    System.err.println("Exception when calling CurrenciesApi#currenciesGetBySymbol");
    e.printStackTrace();
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **symbol** | [**String**](.md)|  |

### Return type

[**CurrencyData**](CurrencyData.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

<a name="currenciesInsert"></a>
# **currenciesInsert**
> currenciesInsert(body)



### Example
```java
// Import classes:
//import io.swagger.client.ApiException;
//import io.swagger.client.api.CurrenciesApi;


CurrenciesApi apiInstance = new CurrenciesApi();
CurrencyData body = new CurrencyData(); // CurrencyData | 
try {
    apiInstance.currenciesInsert(body);
} catch (ApiException e) {
    System.err.println("Exception when calling CurrenciesApi#currenciesInsert");
    e.printStackTrace();
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **body** | [**CurrencyData**](CurrencyData.md)|  |

### Return type

null (empty response body)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: Not defined

<a name="currenciesRateOfExchange"></a>
# **currenciesRateOfExchange**
> Double currenciesRateOfExchange(source, target)



### Example
```java
// Import classes:
//import io.swagger.client.ApiException;
//import io.swagger.client.api.CurrenciesApi;


CurrenciesApi apiInstance = new CurrenciesApi();
String source = "source_example"; // String | 
String target = "target_example"; // String | 
try {
    Double result = apiInstance.currenciesRateOfExchange(source, target);
    System.out.println(result);
} catch (ApiException e) {
    System.err.println("Exception when calling CurrenciesApi#currenciesRateOfExchange");
    e.printStackTrace();
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **source** | [**String**](.md)|  |
 **target** | [**String**](.md)|  |

### Return type

**Double**

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

<a name="currenciesUpdate"></a>
# **currenciesUpdate**
> currenciesUpdate(body)



### Example
```java
// Import classes:
//import io.swagger.client.ApiException;
//import io.swagger.client.api.CurrenciesApi;


CurrenciesApi apiInstance = new CurrenciesApi();
CurrencyData body = new CurrencyData(); // CurrencyData | 
try {
    apiInstance.currenciesUpdate(body);
} catch (ApiException e) {
    System.err.println("Exception when calling CurrenciesApi#currenciesUpdate");
    e.printStackTrace();
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **body** | [**CurrencyData**](CurrencyData.md)|  |

### Return type

null (empty response body)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: Not defined

