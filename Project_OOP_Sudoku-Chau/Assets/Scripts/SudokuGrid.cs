using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SudokuGrid : MonoBehaviour
{
    public int columns = 0;
    public int rows = 0;
    public float square_offset = 0.0f;
    public GameObject grid_square;
    public Vector2 start_position = new Vector2(0.0f, 0.0f);
    public float square_scale = 1.0f;

    private List<GameObject> grid_square_ = new List<GameObject>();
    private int selected_grid_data = -1;
    void Start()
    {
        if (grid_square.GetComponent<GridSquare>() == null)
            Debug.LogError("This Game Object need to have GridSquare script attached!");

        CreateGrid();
        SetGridNumber(GameSettings.Instance.GetGameMode());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void CreateGrid()
    {
        SpawnGridSquares();
        SetSquaresPosition();
    }

    private void SpawnGridSquares()
    {
        //0, 1, 2, 3, 4, 5, 6, 
        //7, 8, ...
        int square_index = 0;
        for (int row = 0; row < rows; row++)
        {
            for (int column = 0; column < columns; column++)
            {
                grid_square_.Add(Instantiate(grid_square) as GameObject);
                grid_square_[grid_square_.Count - 1].GetComponent<GridSquare>().SetSquareIndex(square_index);
                grid_square_[grid_square_.Count - 1].transform.parent = this.transform; //instantiate this game object as a child of the object holding this script
                grid_square_[grid_square_.Count - 1].transform.localScale = new Vector3(square_scale, square_scale, square_scale);

                square_index++;
            }
        }
    }

    private void SetSquaresPosition()
    {
        var square_rect = grid_square_[0].GetComponent<RectTransform>();
        Vector2 offset = new Vector2();
        offset.x = square_rect.rect.width * square_rect.transform.localScale.x + square_offset;
        offset.y = square_rect.rect.height * square_rect.transform.localScale.y + square_offset;

        int column_number = 0;
        int row_number = 0;

        foreach (GameObject square in grid_square_)
        {
            if (column_number +1 > columns)
            {
                row_number++;
                column_number = 0;
            }

            var pos_x_offset = offset.x * column_number;
            var pos_y_offset = offset.y * row_number;
            square.GetComponent<RectTransform>().anchoredPosition = new Vector2(start_position.x + pos_x_offset, start_position.y - pos_y_offset);
            column_number++;
        }
    }

    private void SetGridNumber(string level)
    {
        selected_grid_data = Random.Range(0, SudokuData.Instance.sudoku_game[level].Count);
        var data = SudokuData.Instance.sudoku_game[level][selected_grid_data];

        setGridSquareData(data);
        //foreach (var square in grid_square_)
        //{
        //    square.GetComponent<GridSquare>().SetNumber(Random.Range(0, 10));
        //}
    }

    private void setGridSquareData(SudokuData.SudokuBoardData data)
    {
        for (int index = 0; index < grid_square_.Count; index++)
        {
            grid_square_[index].GetComponent<GridSquare>().SetNumber(data.unsolved_data[index]);
        }
    }
}
