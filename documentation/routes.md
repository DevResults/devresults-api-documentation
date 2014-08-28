[![DevResults](http://devresults.com/Web/Images/logo.gif)](http://devresults.com)

# DevResults API

Once you've configured API access, you're ready to begin calling the endpoints. When calling the DevResults API, there are several things that you'll need to think about before making a request.

## Data Format

The DevResults API supports returning results in multiple formats. You can change the format by setting the `Accept:` header of your request to any of the following:

Format | Value
-------|------
JSON (default) | `Accept: application/json`
CSV            | `Accept: text/csv`

## Parameters

Some of the routes below require parameters. Please refer to this table for their meaning:

Parameter | Description
----------|------------
`{objectName}` | The name of [DevResults object](#objects) to interact with. These can be found below.
`{id}`         | The Id of the [object](#objects) you'd like to retrieve
`{relatedObjectName}` | The name of the related [DevResults object](#objects). You can find the list of valid related objects for a given object below.
`{serializedData}`       | A JSON Serialized copy of the [object](#objects).

Any optional parameters listed in the Routes are passed as query string parameters on the URL.

## Routes

----

### Get All Objects

Field           | Value
----------------|------
**HTTP Verb** | GET
**URL**        | https://instance.devresults.com/api/{objectName}
**Returns**   | Serialized Objects

Optional Parameters

Parameter | Description
----------|------------
`isFull`    | If `false` or omitted, a summary object is returned. If `true`, the complete object is returned.

----

### Get An Object By Id

Field            | Value
-----------------|------
**HTTP Verb** | GET
**URL**        | https://instance.devresults.com/api/{objectName}/{id}
**Returns**   | Serialized Object

Optional Parameters

Parameter | Description
----------|------------
`isFull`    | If `false` or omitted, a summary object is returned. If `true`, the complete object is returned.

----

### Get An Object's Related Objects

Field | Value
-----------------|----
**HTTP Verb**  | GET
**URL**         | https://instance.devresults.com/api/{objectName}/{id}/{relatedObjectName}
**Returns**    | Serialized Objects

Optional Parameters

Parameter | Description
----------|------------
`isFull`    | If `false` or omitted, a summary object is returned. If `true`, the complete object is returned.

----

### Update An Object
Field | Value
------|------
**HTTP Verb**    | PUT
**URL**           | https://instance.devresults.com/api/{objectName}/{id}
**Request Body** | `{'data': {serializedData}}`
**Returns**      | Serialized Updated Object

----

### Create a New Object
Field | Value
------|------
**HTTP Verb**    | POST
**URL**           | https://instance.devresults.com/api/{objectName}
**Request Body** | `{'data': {serializedData}}`
**Returns**      | Serialized Newly Created Object

----

<a name="objects"></a>
## API Objects

Below is the list of DevResults Objects that are currently supported by the API:

* [Awards](#awards)
* [AdminDivisions](#admindivisions)
* [AttributeValues](#attributevalues)
* [Contacts](#contacts)
* [Groups](#groups)
* [Indicators](#indicators)
* [IndicatorResults](#indicatorresults)
* [Locations](#locations)
* [Organizations](#organizations)
* [Photos](#photos)
* [ReportingPeriods](#reportingperiods)
* [Results](#results)
* [ResultsFrameworks](#resultsframeworks)
* [Sectors](#sectors)
* [Tags](#tags)

----

<a name="awards"></a>
### Awards (Activities)

**Format:**
```
{  
    "AwardID": int,
    "Title": string,
    "ShortTitle": string,
    "Code": string,
    "ReferenceNumber": string,
    "OrganizationID": int,
    "AwardingOrganizationID": int,
    "StatusOptionID": int,
    "AwardSubTypeID": int,
    "PrimaryContactID": int
 }
```

**Related Objects:**

* [AdminDivisions](#admindivisions)
* [Locations](#locations)
* [Results](#results)
* [ResultsFrameworks](#resultsframeworks)
* [Sectors](#sectors)
* [Tags](#tags)

----

<a name="admindivisions"></a>
### AdminDivisions

**Format:**
```
{  
   "AdminDivisionID": int,
   "Title": string,
   "Level": int,
   "Lineage": string,
   "TitleLineage": string,
   "FullTitle": string,
   "AdminDivisionSetID": int,
   "AdminDivisionLevelID": int
}
```

**Related Objects:**

None

----

<a name="attributevalues"></a>
### AttributeValues

**Format:**
```
{  
   "AttributeValueID": int,
   "Title": string,
   "AttributeID": int,
   "DisplayOrder": int
}
```

**Related Objects:**

None

----

<a name="contacts"></a>
### Contacts

**Format:**
```
{  
   "ContactID": int,
   "Email": string,
   "FullName": string,
   "LastFirst": string,
   "OrganizationID": int
}
```

**Related Objects:**

* [Groups](#groups)

----

<a name="groups"></a>
### Groups

**Format:**
```
{  
   "GroupID": int,
   "Title": string
}
```

**Related Objects:**

None

----

<a name="indicators"></a>
### Indicators

**Format:**
```
{  
   "IndicatorID": int,
   "Title": string,
   "Code": string,
   "CodeAndTitle": string,
   "UnitID": int,
   "AdminDivisionLevelID": int,
   "DataFormat": string,
   "Calculation": string
}
```

**Related Objects:**

* [Results](#results)
* [Sectors](#sectors)
* [Tags](#tags)

----

<a name="indicatorresults"></a>
### IndicatorResults

**Format:**
```
{  
   "IndicatorResultID": int,
   "EffectiveDate": datetime,
   "Result": number,
   "IndicatorID": int,
   "AwardID": int,
   "ReportingPeriodID": int,
   "LocationID": int,
   "AdminDivisionID": int
}
```

**Related Objects:**

* [AttributeValues](#attributevalues)

----

<a name="locations"></a>
### Locations

**Format:**
```
{  
   "LocationID": int,
   "Title": string,
   "TitleAndAdminDivision": string,
   "FacilityCode": string,
   "Title_Lat": number,
   "Title_Lng": number,
   "AdminDivisionID": int
}
```

**Related Objects:**

* [AdminDivisions](#admindivisions)
* [Tags](#tags)

----

<a name="organizations"></a>
### Organizations

**Format:**
```
{  
   "OrganizationID": int,
   "Title": string,
   "ShortTitle": string
}
```

**Related Objects:**

* [Tags](#tags)

----

<a name="photos"></a>
### Photos

**Format:**
```
{  
   "PhotoID": int,
   "Alt": string,
   "Caption": string,
   "AwardID": int,
   "PhotoFolderID": int
}
```

**Related Objects:**

* [Tags](#tags)

----

<a name="reportingperiods"></a>
### ReportingPeriods

**Format:**
```
{  
   "ReportingPeriodID": int,
   "Title": string,
   "StartDate": datetime,
   "EndDate": datetime,
   "SubmissionStartDate": datetime,
   "SubmissionEndDate": datetime,
   "IsInterim": boolean,
   "ResultsFrameworkID": int,
   "ResultID": int
}
```

**Related Objects:**

* [Results](#results)

----

<a name="results"></a>
### Results

**Format:**
```
{  
   "ResultID": int,
   "Title": string,
   "Code": string,
   "CodeAndTitle": string,
   "ResultsFrameworkID": int,
   "FullCodeAndTitle": string
}
```

**Related Objects:**

None

----

<a name="resultsframeworks"></a>
### ResultsFrameworks

**Format:**
```
{  
   "ResultsFrameworkID": int,
   "Code": string,
   "Title": string
}
```

**Related Objects:**

None

----

<a name="sectors"></a>
### Sectors

**Format:**
```
{  
   "SectorID": int,
   "Title": string
}
```

**Related Objects:**

None

----

<a name="tags"></a>
### Tags

**Format:**
```
{  
   "TagID": int,
   "Title": string,
   "ObjectName": string
}
```

**Related Objects:**

None
