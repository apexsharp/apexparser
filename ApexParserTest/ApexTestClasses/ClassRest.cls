@RestResource(urlMapping='/api/v1/RestDemo')
global class ClassRest
{
    @httpDelete
    global static void DoDelete() {
    }

    @httpPost
    global static void Post() {
    }

    @httpGet
    global static string Get() {
        return 'Jay';
    }

    @httpPatch
    global static void Patch() {
    }

    @httpPut
    global static void Put() {
    }
}