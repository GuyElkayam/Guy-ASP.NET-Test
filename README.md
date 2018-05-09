[![Stories in Ready](https://badge.waffle.io/giladDAN/AspNetInterviewTest.png?label=ready&title=Ready)](https://waffle.io/giladDAN/AspNetInterviewTest)
# AspNetInterviewTest

פרוייקט דוגמא עבור ראיון עבודה
An example project to test .net Data structure.
* first stage .net Data structure
* second stage is working with entity framework and localdb to get some insights.

## תיאור הבעיה The Problem
The System Recevices an array of integars, each int represent a status code of an event. 
the events are numbered between 1 - 100 but the size of the array is about |10000|.

The Business logic class has one function called Next

Next function has this requirements

1. get a postion in the array
2. return the next postion of the value of the item( the next  postion where the number at)
3. the next postion is going "right" nextpostion > currentPostions


## Requeriemnts
1. return an answer as O(1) - not running on the the array.
2. check for Edge Cases ( Empty array, wrong postion)


## Location of code

1. EventsRepository has a function called Next, you need to implemnt it and return the next postion
2. EventsRepository has a list of int - Items this is the values you need to work on.
3. The function is called using the calc API
4. You can test the code using the Home controller and the next number
5. If the items are changed each call you can save it in memmory somehow.

## a simple check


var arr = [ 1,2,8,1,2,3,5,6,7,2,2,9,9,9,3,2,1,2,3];
// the number are from 1- 10


examples 
* getNewPostion(0) return 3   (the number is 1)
* getNewPostion(3) return 16  (the number is  1)
* getNewPostion(1) return 4   (the number is 2)
* getNewPostion(2) return ?

# Stage 2 Add DB with Entity Framework

## adding Support For MVC and Entity framework 

in the solution there is a local db called Database1, this db have a table called SelectedItems
this table keep the array of intagers from the EventRepository. the site need to add some helper function that show information to the Admin
 _DataRepository.SaveItems(Items);

## the mission
1. Create new Controller name backoffice
2.in the back office controller add support to call DataRepository
3. implement the two funciton - that returns a list of number from 1-100 and the count from the table
    (2,100) means that the number 2 appears 100 times, also 3,0 means that the number 3 doesn't appear.
    1.GetItemsAccroding
    2.GetItemsAndCount
4. show the result in a simple razor view

## help
you can add a view, stored procedure or work with linq. also if needed to add a table allborate why.




