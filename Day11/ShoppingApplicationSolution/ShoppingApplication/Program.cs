using ShoppingApplicationModelLibrary;
using ShoppingBLLibrary.BL;
using ShoppingDALLibrary;
using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace ShoppingApplication
{
    public class Program
    {
        private ProductBL _productService;
        private CustomerBL _customerService;
        private CartBL _cartService;
        private CartItemBL _cartItemService;

        public Program()
        {
            _productService = new ProductBL();
            _customerService = new CustomerBL();
            _cartItemService = new CartItemBL();
            _cartService = new CartBL();
            
        }
        public static void Main(string[] args)
        {
            Program program = new Program();
            program.Run();
        }

        private void Run()
        {
            while (true)
            {
                DisplayMenu();

                switch (Console.ReadLine())
                {
                    case "1":
                        ManageCustomers();
                        break;
                    case "2":
                        ManageProducts();
                        break;
                    case "3":
                        ManageCart();
                        break;
                    case "4":
                        return;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        private void ManageCart()
        {
            while (true)
            {
                Console.WriteLine("\nManage Cart:");
                Console.WriteLine("1. Add to Cart");
                Console.WriteLine("2. Get all items in Cart");
                Console.WriteLine("3. Get Total price in cart");
                Console.WriteLine("4. Update items in cart");
                Console.WriteLine("5. Delete items from cart");
                Console.WriteLine("6. View cart details");
                Console.WriteLine("7. Delete cart");
                Console.WriteLine("8. Back to Main Menu");
                Console.Write("Choose an option: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        AddToCart();
                        break;
                    case "2":
                        GetAllCartItems();
                        break;
                    case "3":
                        GetTotalPrice();
                        break;
                    case "4":
                        //UpdateCartItems();
                        break;
                    case "5":
                        //DeleteCartItems();
                        break;
                    case "6":
                        ViewAllCarts();
                        break;
                    case "7":
                        DeleteCart();
                        break;
                    case "8":
                        return;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        private void DeleteCart()
        {
            try
            {
                Customer customer = GetCustomerFromUser();

                if (customer == null)
                {
                    Console.WriteLine("Customer with that ID does not exist");
                    ManageCart();
                }

                var result = _cartService.DeleteCart(customer.CartId);
                if (result == null)
                {
                    Console.WriteLine("Error occured!Try Again!");
                    ManageCart();
                }
                customer.CartId = -1;
                _customerService.UpdateCustomer(customer);
                Console.WriteLine($"Customer {customer.Id}'s cart has been deleted!");
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred while deleting the cart items: {e.Message}");
            }
        }

        private void GetTotalPrice()
        {
            try
            {
                Customer customer = GetCustomerFromUser();

                if (customer == null)
                {
                    Console.WriteLine("Customer with that ID does not exist");
                    ManageCart();
                }
                Cart cart = _cartService.GetCartById(customer.CartId);

                Console.WriteLine("The total price of the cart is Rs. " + cart.TotalPrice);
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred while retrieving the cart price: {e.Message}");
            }
        }

        private void ViewAllCarts()
        {
            try
            {
                var carts = _cartService.GetAllCarts();
                foreach (var cart in carts)
                {
                    Console.WriteLine(cart);
                    Console.WriteLine("-----------");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occured {ex.Message}");
            }
        }

        private void GetAllCartItems()
        {
            try
            {
                Customer customer = GetCustomerFromUser();

                if (customer == null)
                {
                    Console.WriteLine("Customer with that ID does not exist");
                    ManageCart();
                }

                Cart cart = _cartService.GetCartById(customer.CartId);

                Console.WriteLine("Items in cart:");
                foreach (var item in cart.CartItems)
                {
                    Console.WriteLine(item.Product.Name + "\tRs." + item.Price);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred while retrieving the cart items: {e.Message}");
            }

        }

        private Customer GetCustomerFromUser()
        {
            try
            {
                Customer customer = null;

                Console.WriteLine("Enter Customer Id: ");
                string stringId = Console.ReadLine();
                if (!string.IsNullOrEmpty(stringId))
                {
                    int id = int.Parse(stringId);

                    customer = _customerService.GetCustomerById(id);

                    if(customer != null)
                    {
                        return customer;
                    }
                }
                else
                {
                    Console.WriteLine("Please try again with a valid id");
                }
            }
            catch(Exception e)
            {
                Console.WriteLine($"An error occurred: {e.Message}");
            }
            return null;
        }

        private int GetUserChoice()
        {
            int id;
            while (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine("Enter an valid Id");
                Console.WriteLine("Enter the value again : ");
            }
            return id;
        }

        private void AddToCart()
        {
            try
            {
                Customer customer = GetCustomerFromUser();

                if (customer == null)
                {
                    Console.WriteLine("Customer with that ID does not exist");
                    ManageCart();
                }

                Cart cart;

                //if (customer.CartId == -1)
                //{
                cart = new Cart() { CustomerId = customer.Id, Customer = customer };
                //}
                //else
                //{
                //    cart = _cartService.GetCartById(customer.CartId);
                //}

                int userChoice = 1;

                List<CartItem> cartItems = new List<CartItem>();

                while (userChoice == 1)
                {
                    ViewAllProducts();

                    Console.WriteLine("Enter the product Id to be added: ");

                    int pid = GetUserChoice();

                    Console.WriteLine("Enter the quantity of the product: ");
                    int quantity = GetUserChoice();

                    Product product = _productService.GetProductById(pid);
                    if (product.QuantityInHand <= 0)
                    {
                        Console.WriteLine("Product is out of stock. Please choose another product!!");
                        ManageCart();
                    }
                    while (quantity > product.QuantityInHand && product.QuantityInHand != 0)
                    {
                        Console.WriteLine($"Only {product.QuantityInHand} number of items left!!");
                        Console.WriteLine("Enter the quantity of the product: ");
                        quantity = GetUserChoice();
                    }

                    CartItem cartItem = new CartItem()
                    {
                        CartId = cart.Id,
                        ProductId = pid,
                        Product = product,
                        Quantity = quantity,
                        Price = product.Price,
                        Discount = 0,
                        PriceExpiryDate = DateTime.MaxValue
                    };

                    _cartItemService.AddCartItem(cartItem);

                    cartItems.Add(cartItem);

                    Console.WriteLine("Do you want to add more products? (1 for Yes, 0 for No): ");
                    userChoice = GetUserChoice();
                }

                cart.CartItems = cartItems;

                int cartId = _cartService.AddCart(cart);
                customer.CartId = cartId;
            }
            catch(Exception ex)
            {
                Console.WriteLine($"An error occured {ex.Message}");
            }
        }

        private void ManageProducts()
        {
            while (true)
            {
                Console.WriteLine("\nManage Products:");
                Console.WriteLine("1. Add Product");
                Console.WriteLine("2. Get Product By ID");
                Console.WriteLine("3. Get Product By Name");
                Console.WriteLine("4. Update Product");
                Console.WriteLine("5. Delete Product");
                Console.WriteLine("6. View All Products");
                Console.WriteLine("7. Back to Main Menu");
                Console.Write("Choose an option: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        AddProduct();
                        break;
                    case "2":
                        GetProductById();
                        break;
                    case "3":
                        GetProductByName();
                        break;
                    case "4":
                        UpdateProduct();
                        break;
                    case "5":
                        DeleteProduct();
                        break;
                    case "6":
                        ViewAllProducts();
                        break;
                    case "7":
                        return;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        private void ViewAllProducts()
        {
            Console.WriteLine("\nAll Products:");
            var products = _productService.GetAllProducts();
            foreach (var product in products)
            {
                Console.WriteLine(product);
            }
        }

        private void DeleteProduct()
        {
            try
            {
                Console.WriteLine("Enter Product ID to be deleted: ");
                int id = int.Parse(Console.ReadLine()!);

                Product result = _productService.DeleteProduct(id);
                Console.WriteLine($"Product {result.Name} with ID {result.Id} has been deleted");
            }
            catch (Exception e)
            {

                Console.WriteLine($"An error occurred while deleting the product: {e.Message}");
            }
        }

        private void UpdateProduct()
        {
            try
            {
                Console.Write("Enter Product ID: ");
                int id = int.Parse(Console.ReadLine()!);

                var product = _productService.GetProductById(id);

                if (product != null)
                {
                    Console.WriteLine("Current Product Details:");
                    Console.WriteLine(product);

                    Console.Write("Enter new Product Name (leave blank to keep current): ");
                    string? newName = Console.ReadLine();
                    if (!string.IsNullOrEmpty(newName))
                    {
                        product.Name = newName;
                    }

                    Console.Write("Enter new Price (leave blank to keep current): ");
                    string newPrice = Console.ReadLine();
                    if (!string.IsNullOrEmpty(newPrice))
                    {
                        product.Price = double.Parse(newPrice);
                    }

                    Console.Write("Enter new Image (leave blank to keep current): ");
                    string? newImage = Console.ReadLine();
                    if (!string.IsNullOrEmpty(newImage))
                    {
                        product.Image = newImage;
                    }

                    Console.Write("Enter new Quantity (leave blank to keep current): ");
                    string newQuantity = Console.ReadLine();
                    if (!string.IsNullOrEmpty(newQuantity))
                    {
                        product.QuantityInHand = int.Parse(newQuantity);
                    }



                    _productService.UpdateProduct(product);
                    Console.WriteLine("Product updated successfully.");
                }
                else
                {
                    Console.WriteLine($"Product with ID {id} not found.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred while updating the customer: {e.Message}");
            }
        }

        private void GetProductByName()
        {
            try
            {
                Console.Write("Enter Product name: ");
                string name = Console.ReadLine()!;

                Product product = _productService.GetProductByName(name);
                if (product != null)
                {
                    Console.WriteLine(product);
                }
                else
                {
                    Console.WriteLine("Product not found.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred while retrieving the product: {e.Message}");
            }
        }

        private void GetProductById()
        {
            try
            {
                Console.Write("Enter Product ID: ");
                int id = int.Parse(Console.ReadLine()!);

                Product product = _productService.GetProductById(id);
                if (product != null)
                {
                    Console.WriteLine(product);
                }
                else
                {
                    Console.WriteLine("Product not found.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred while retrieving the product: {e.Message}");
            }
        }

        private void AddProduct()
        {
            try
            {
                Console.Write("Enter Product Name: ");
                string name = Console.ReadLine()!;
                Console.Write("Enter Price: ");
                double price = Convert.ToDouble(Console.ReadLine());
                Console.Write("Enter Image URL: ");
                string image = Console.ReadLine()!;
                Console.Write("Enter Quantity in Hand: ");
                int quantityInHand = int.Parse(Console.ReadLine()!);

                Product product = new Product
                {
                    Name = name,
                    Price = price,
                    Image = image,
                    QuantityInHand = quantityInHand
                };
                int productId = _productService.AddProduct(product);
                Console.WriteLine($"Product added with ID: {productId}");
            }
            catch (Exception e)
            {

                Console.WriteLine($"An error occurred while adding the product: {e.Message}");
            }
        }

        private void DisplayMenu()
        {
            Console.WriteLine("\nMenu:");
            Console.WriteLine("1. Manage Customers");
            Console.WriteLine("2. Manage Products");
            Console.WriteLine("3. Do Shopping");
            Console.WriteLine("4. Exit");
            Console.Write("Choose an option: ");
        }

        private void ManageCustomers()
        {
            while (true)
            {
                Console.WriteLine("\nManage Customers:");
                Console.WriteLine("1. Add Customer");
                Console.WriteLine("2. Get Customer By ID");
                Console.WriteLine("3. Get Customer By Name");
                Console.WriteLine("4. Update Customer");
                Console.WriteLine("5. Delete Customer");
                Console.WriteLine("6. View All Customers");
                Console.WriteLine("7. Back to Main Menu");
                Console.Write("Choose an option: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        AddCustomer();
                        break;
                    case "2":
                        GetCustomerById();
                        break;
                    case "3":
                        GetCustomerByName();
                        break;
                    case "4":
                        UpdateCustomer();
                        break;
                    case "5":
                        DeleteCustomer();
                        break;
                    case "6":
                        ViewAllCustomers();
                        break;
                    case "7":
                        return;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        private void ViewAllCustomers()
        {
            Console.WriteLine("\nAll Customers:");
            var customers = _customerService.GetAllCustomers();
            foreach (var customer in customers)
            {
                Console.WriteLine(customer);
            }
        }

        private void DeleteCustomer()
        {
            try
            {
                Console.WriteLine("Enter Customer ID to be deleted: ");
                int id = int.Parse(Console.ReadLine()!);

                Customer result = _customerService.DeleteCustomer(id);
                Console.WriteLine($"Customer {result.Name} with ID {result.Id} has been deleted");
            }
            catch (Exception e)
            {

                Console.WriteLine($"An error occurred while deleting the customer: {e.Message}");
            }
        }

        private void UpdateCustomer()
        {
            try
            {
                Console.Write("Enter Customer ID: ");
                int id = int.Parse(Console.ReadLine()!);

                var customer = _customerService.GetCustomerById(id);

                if (customer != null)
                {
                    Console.WriteLine("Current Customer Details:");
                    Console.WriteLine(customer);

                    Console.Write("Enter new Customer Name (leave blank to keep current): ");
                    string? newName = Console.ReadLine();
                    if (!string.IsNullOrEmpty(newName))
                    {
                        customer.Name = newName;
                    }

                    Console.Write("Enter new Age (leave blank to keep current): ");
                    string newAge = Console.ReadLine();
                    if (!string.IsNullOrEmpty(newAge))
                    {
                        customer.Age = int.Parse(newAge);
                    }

                    Console.Write("Enter new Phone No (leave blank to keep current): ");
                    string? newPhoneNo = Console.ReadLine();
                    if (!string.IsNullOrEmpty(newPhoneNo))
                    {
                        customer.Phone = newPhoneNo;
                    }

                    

                    _customerService.UpdateCustomer(customer);
                    Console.WriteLine("Customer updated successfully.");
                }
                else
                {
                    Console.WriteLine($"Customer with ID {id} not found.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred while updating the customer: {e.Message}");
            }
        }

        private void GetCustomerByName()
        {
            try
            {
                Console.Write("Enter Customer name: ");
                string name = Console.ReadLine()!;

                Customer customer = _customerService.GetCustomerByName(name);
                if (customer != null)
                {
                    Console.WriteLine(customer);
                }
                else
                {
                    Console.WriteLine("Customer not found.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred while retrieving the customer: {e.Message}");
            }
        }

        private void GetCustomerById()
        {
            try
            {
                Console.Write("Enter Customer ID: ");
                int id = int.Parse(Console.ReadLine()!);

                Customer customer = _customerService.GetCustomerById(id);
                if (customer != null)
                {
                    Console.WriteLine(customer);
                }
                else
                {
                    Console.WriteLine("Customer not found.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred while retrieving the customer: {e.Message}");
            }
        }

        private void AddCustomer()
        {
            try
            {
                Console.Write("Enter Customer Name: ");
                string name = Console.ReadLine()!;
                Console.Write("Enter Age: ");
                int age = int.Parse(Console.ReadLine()!);
                Console.Write("Enter Phone No: ");
                string phone = Console.ReadLine()!;
                
                Customer customer = new Customer
                {
                    Name = name,
                    Age = age,
                    Phone = phone,
                };
                int customerId = _customerService.AddCustomer(customer);
                Console.WriteLine($"Customer added with ID: {customerId}");
            }
            catch (Exception e)
            {

                Console.WriteLine($"An error occurred while adding the customer: {e.Message}");
            }
        }
    }


}
