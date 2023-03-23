

PROD
                  FW
------------+      |
            |      |   
DB STATIC   |      |
            |      |
------------+

INT TEST

/api/products -> [{id:1, title: 2, amount: 3}]
/api/products/2 -> {id:1, title: 2, amount: 3}
/api/users


------------+      +----------------+
            |      |                |
DB DYNAMIC  |      |       API      |
            |      +----------------+
------------+

EE TESTS

------------+      +----------------+  +------------+
            |      |                |  |     FE     |
DB TEMP     |      |       API      |  |            |
            |      +----------------+  +-------------
------------+

LOAD TESTS
------------+      +----------------+  
            |      |                |  
DB TEMP     |      |       API      |  X10 
            |      +----------------+  
------------+

X100000
/api/products -> [{id:1, title: 2, amount: 3}]

api:
auth:
front: