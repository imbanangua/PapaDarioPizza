# PapaDarioPizza

Like any online pizza ordering system, in Papa Dario’s Pizza App, users can select pizza’s of various sizes, and toppings and there are many combos and deals of the day to choose from. Of course Papa Dario’s also sells wings, sandwiches and poutine.

Aside from offering great food at great prices, Papa Dario’s offers members of Papa Dario’s Fan Club an additional 10% discount on all online orders. Of course anyone who is not a member can easily become one by filling in an online registration form.

Papa Dario’s also knows some customers order the same things over and over again and so users should be able to select if they want to order the same items from a previous order (which they should be able to view to verify).

Papa Dario’s app also includes information about Papa Dario’s Pizza such as it’s origin, some information about Papa Dario’s Food Emporium and contact information as well.

Papa Dario's Pizza cares about user feedback and so users can be able to provide feed back and suggestions which admin of Papa Dario’s can be able to view.

After placing orders, registered users will receive an online receipt to their email. Non-registered users will receive an online receipt to the email they provide.

## Some programming logic

I use three global variables, g_shoppingcart, g_member and g_admin. They are ShoppingCart object, Member object and Admin object separately. When customs add products to shopping cart, products will be added to g_shoppingcart. Every time navigate to shopping cart page, the products in g_shoppingcart will be added to an ObservableCollection of ShoppingCartItem object. Every product is a ShoppingCartItem object. The products in ShoppingCartItem are be bind to the listview in shopping cart page. HostViewModel object will calculate the price based on the product in g_shoppingcart. Then, use it to bind price in shopping cart page. Every time remove an item from shopping cart, it will use Index property to find which item should be remove and remove it from g_shoppingcart. Then, products in g_shoppingcart will be added to ShoppingCartItem ObservableCollection again and refresh the listview and the price.
  
If a member login, a Member object will be saved in g_member. As long as the name property of g_member is not null, HostViewModel object will apply a 10% discount to the price. Then, the price with discount will be shown in shopping cart page. When checkout, an email in profile will be used if member has login. Otherwise, customer need to enter an email for receiving online receipt.

## About pages

**MainPage.xaml** is a page that has a navigation menu to navigate to each page. It has a Frame that is used to display other pages within MainPage.

**OrderPizza.xaml** is a default page for this application. Customer can order products in this page, and add them to shopping cart. Also, they can login their accounts, or go to registration page to register a new one.

**RegistrationPage.xaml** is a page for registration. Customer can register new accounts in this page.

**MyProfile.xaml** is a page for members to change their email or password.

**ShoppingCartPage.xaml** is a shopping cart page. It will display all the items that customers added to shopping cart. The price will also be shown. Than they can check out their order.

**Feedback.xaml** is a page that customer can send their feedbacks.

**AboutUs.xaml** is a page to show the information about Papa Dario’s Pizza. It includes the origin and contact information.

**AdminLogin.xaml** is a page for administrators to login, and then they can do CRUD operations.

**AdminView.xaml** is a page for administrators to do CRUD operations. After administrators login, this page will be navigated to by Frame in AdminLogin.xaml.

**BlankPage.xaml** is a page used when administrators do not log in. When no administrator login, this page will display within AdminLogin.xaml.

## About classes

Admin.cs is a class of administrator role object. It has Name and Password properties.

Member.cs is a class of Papa Dario’s Pizza member object. It has Name and Password properties.

Deal.cs is a class for today’s deal object. It has Name, Price and Quantity properties. When initialize it, it has a name and an original price 12.99.

Pizza.cs is a class for pizza object. It has Type, Size, Topping array, Combo, Drink, Side and Price properties. Price will be based on what size you choose, what toppings you choose and whether you select combo or not.

Poutine.cs is a class for poutine object. It has Name, Price and Quantity properties. When initialize it, it has a name and an original price 6.99.

Sandwiches.cs is a class for sandwiches object. It has Name, Price and Quantity properties. When initialize it, it has a name and an original price 7.99.
  
Wings.cs is a class for wings object. It has Name, Price and Quantity properties. When initialize it, it has a name and an original price 9.99 for 5 pieces.

ShoppingCart.cs is a class for shopping cart object. This object will be used to create a shopping cart instance when customer add products to shopping cart. It has Deal, Wings, Sandwiches, Poutine and Pizza ArrayList properties. They all are objects from above classes.

ShoppingCartItem.cs is a class for binding data to listview in shopping cart page. It has Name, Price, Quantity, Index and PizzaIndex properties, Index is used to indicate the index of item in shopping cart listview. PizzaIndex is to indicate the index of pizza in Pizza ArrayList.

HostViewModel.cs is a class for binding data to price field in shopping cart page. It has Subtotal, Tax and Total properties. Also, it has a ButtonEnable properties which is used to disable the Checkout button when the shopping cart is empty.

DatabaseAccess.cs is an object to store some database operation methods:
* AddMember: Add member to database.
* AddFeedback: Add feedback to database.
* AddReceipt: Add receipt to database.
* IsExist: Determine whether the member exists.
* IsPasswordCorrect: Determine whether the member password is correct.
* SelectEmail: Return member’s email in database.
* DeleteMember: Delete member from database.
* UpdateMember: Update member’s information in database.
* DeleteFeedback: Delete feedback in database.
* IsAdminExist: Determine whether the administrator exists.
* IsAdminPasswordCorrect: Determine whether the administrator password is correct.

MemberList.cs is a class of the object that is used to bind data to member list. It has Name, Email and Password properties. GetMembers method is to get a member list that will be displayed in a listview.

FeedbackList.cs is a class of the object that is used to bind data to feedback list. It has Id, Email and Feedback properties. GetFeedbacks method is to get a feedback list that will be displayed in a listview.
