# Http Request Reporter

This project is created to read the HTTP logs and generate a report to find the unique visitor/IP Addresses, most popular URLs and the most active IPs.
The solution can process one or more files in a single run. The result produced would include data from all of the files uploaded, and would throw exceptions if all files uploaded are empty or contains data which doesn't follow the below format:

177.71.128.21 - - [10/Jul/2018:22:21:28 +0200] "GET /intranet-analytics/ HTTP/1.1" 200 3574

## Development

This solution uses .Net 8, and follows Web API development standards. Currently it exposes only one endpoint, but can be extended to support more functionality if required.
We can also add support for more log formats, by utilizing the exitsing helper class to parse the data.

## Testing
Two seperate test projects are present in the solution, one for API/controller code and another one is for Service/Business layer. Both serves different purposes and needs to be maintained in future.


