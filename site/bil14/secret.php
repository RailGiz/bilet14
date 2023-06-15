<?php

require_once "common/Page.php";
use common\Page;
use common\DbHelper;

class secret extends Page
{
    protected function showContent()
    {
        $dbHelper = DbHelper::getInstance();
        $coefficients = $dbHelper->getSLAUCoefficients();

        if (!empty($coefficients)) {
            echo '<link rel="stylesheet" href="css/main.css">'; // Подключение внешнего CSS-файла

            echo "<table>";
            echo "<tr><th>a</th><th>b</th><th>c</th><th>d</th><th>e</th><th>f</th><th>g</th><th>h</th><th>i</th><th>j</th><th>k</th><th>l</th><th>x1</th><th>x2</th><th>x3</th></tr>";

            foreach ($coefficients as $row) {
                echo "<tr>";
                echo "<td><div class='value-block'>".$row['a']."</div></td>";
                echo "<td><div class='value-block'>".$row['b']."</div></td>";
                echo "<td><div class='value-block'>".$row['c']."</div></td>";
                echo "<td><div class='value-block'>".$row['d']."</div></td>";
                echo "<td><div class='value-block'>".$row['e']."</div></td>";
                echo "<td><div class='value-block'>".$row['f']."</div></td>";
                echo "<td><div class='value-block'>".$row['g']."</div></td>";
                echo "<td><div class='value-block'>".$row['h']."</div></td>";
                echo "<td><div class='value-block'>".$row['i']."</div></td>";
                echo "<td><div class='value-block'>".$row['j']."</div></td>";
                echo "<td><div class='value-block'>".$row['k']."</div></td>";
                echo "<td><div class='value-block'>".$row['l']."</div></td>";
                echo "<td><div class='value-block'>".$row['x1']."</div></td>";
                echo "<td><div class='value-block'>".$row['x2']."</div></td>";
                echo "<td><div class='value-block'>".$row['x3']."</div></td>";
                echo "</tr>";
            }
            

            echo "</table>";
        } else {
            echo "<p>No data available.</p>";
        }
    }
}

(new secret())->show();
