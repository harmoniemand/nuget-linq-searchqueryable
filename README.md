
Enables you to use a linq-style search extension-method over a queryable.

    IQueryable<SampleClass> query;
    query = query.Search("searchstring");

This simple code will run over every property, that is not virtual, to check for the searchstring using ToString.
Also works on Linq DB Queries.
