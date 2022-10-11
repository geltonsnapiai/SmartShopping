import React from 'react'

const ReadOnlyRow = ({ data, handleEditClick, handleDeleteClick }) => {
  return (
    <tr>

        <td>{data.store}</td>
        <td>{data.item}</td>
        <td>{data.price}</td>
        <td>{data.date}</td>
        <button type="button" class="btn btn-square btn-secondary m-1" onClick={(event)=> handleEditClick(event, data)}>
            <i class="fa fa-pen"/>
        </button>
        <button type="button" class="btn btn-square btn-secondary m-1" onClick={() => handleDeleteClick(data.id)}>
            <i class="fa fa-trash"/>
        </button>
    </tr>
  )
}

export default ReadOnlyRow