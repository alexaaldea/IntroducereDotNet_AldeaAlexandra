What happens if you apply pagination after materializing the query with .ToList()? Why is this
problematic for large datasets?
-> memory problems (all rows are loaded into memory first, even if you only need 10 or 20)
-> inefficient database usage
-> performance issues (paging is much slower in memory)
