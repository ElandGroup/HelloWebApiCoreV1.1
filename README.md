# Api For .NET CORE V2

The overriding design goal for api is to make it as unified as possible

Api format:
```sh
http://10.202.101.25:8090/display/HQ/API+Format
```

Notes:
  - Api format
  - Api Sample

### Api format

Default Response

```sh
  success:	true or false
  result:	result data
  error:	error code, message
```

  Examples
```sh
  Success
  {
      "success": true,
      "result": {
          "name": "Lake Inc",
          "price": 840,
          "creationTime": "2016-06-27T23:00:38.103+08:00",
          "creatorUserId": null,
          "id": 1
      },
      "error": null
  }
```
```sh
  Fail
  {
      "success": false,
      "result": null,
      "error": {
          "code": 0,
          "message": "An internal error occurred during your request!",
          "details": null,
          "validationErrors": null
      }
  }
```


### Api Sample

api:
```sh
http://netcore.api.elandcloud.com/api/v2/ping
```
  - C: http://netcore.api.elandcloud.com/api/v2/fruit
  
  ```sh
  Param:
   {
      "name": "Apple3",
      "price": 15,
      "color": "Red",
      "code": "A5",
      "storeCode": null
    }
   Result:
    {
      "success": true,
      "result": null,
      "error": null
    }
  ```
  
  - R(all): http://netcore.api.elandcloud.com/api/v2/fruit
  
    ```sh
    Result:
    {
      "success": true,
      "result": [
        {
          "name": "Pear",
          "price": 12,
          "color": "Yellow",
          "code": "A2",
          "storeCode": null
        },
        {
          "name": "Apple",
          "price": 16,
          "color": "Red",
          "code": "A3",
          "storeCode": null
        }
      ],
      "error": null
    }
  ```
  
  - R(one): http://netcore.api.elandcloud.com/api/v2/fruit/apple
  
```sh 
    result:
    {
      "success": true,
      "result": {
        "name": "Apple",
        "price": 16,
        "color": "Red",
        "code": "A3",
        "storeCode": null
      },
      "error": null
    }
  ```
  
  - U: http://netcore.api.elandcloud.com/api/v2/fruit/apple
  
```sh
  Param:
   {
      "name": "Apple",
      "price": 17,
      "color": "Red",
      "code": "A3",
      "storeCode": null
    }
   Result:
    no content
  ```
  
  - D: http://netcore.api.elandcloud.com/api/v2/fruit/apple
  
```sh
   Result:
   no content
  ```
  
Thank you















