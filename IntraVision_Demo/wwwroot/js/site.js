
console.log('Задание выполнил А.Бубнов, возьмите на работу пж(https://career.habr.com/alexanderbubnov)');
$(document).ready(function () {
    $('.SetUserBalance').click(function () {
        var currentRow = $(this).closest("tr");
        var coinId = currentRow.find("td:eq(3)").html();
        $.post("/Home/SetUserBalance?coinId=" + coinId, function (data) {
            $("#UserBalance").html(data);
        });
    });


    $('.BuyDrink').click(function () {
        var currentRow = $(this).closest("tr");
        var drinkId = currentRow.find("td:eq(5)").html();
        var drinkCount = currentRow.find("td:eq(3)").html();
        $.post("/Home/BuyDrink?drinkId=" + drinkId, function (data) {
            var msg = '';
            if (data.success) {
                msg += 'Вы купили: ' + data.drink.name + '\n';
                if (data.change.length > 0) {
                    msg += 'Ваша сдача: \n';
                    data.change.forEach(function (item, index, array) {
                        msg += item.count + ' монеты по ' + item.amount + '\n';
                    });
                }
                $("#UserBalance").html(0);
                var remainder = drinkCount - 1;
                if (remainder > 0) {
                    currentRow.find("td:eq(3)").html(remainder);
                }
                else {
                    currentRow.remove();
                }
            }
            else {
                msg = 'не получилось';
            }
            alert(msg);
        });
    });
});