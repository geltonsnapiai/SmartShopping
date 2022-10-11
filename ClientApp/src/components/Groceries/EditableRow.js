import React from 'react'
import { Input } from "reactstrap";

const EditableRow = ({handleCancelClick, editFormData, handleEditFormChange}) => {
  return (
    <tr>

        <td>
            <select
                required
                class="form-select"
                name="store"
                placeholder="shop"
                value={editFormData.store}
                onChange={handleEditFormChange}
            >
                <option></option>
                <option>Iki</option>
                <option>Lidl</option>
                <option>Maxima</option>
                <option>Norfa</option>
                <option>Rimi</option>
            </select>
        </td>

        <td>
            <Input
            required
            type="text"
            name="item"
            placeholder="Enter product..."
            value={editFormData.item}
            onChange={handleEditFormChange}
            />
        </td>

        <td>
            <Input
            required
            type="number"
            step="0.01"
            name="price"
            placeholder="Enter price..."
            value={editFormData.price}
            onChange={handleEditFormChange}
            />
        </td>

        <td>
            <Input
            required
            type="date"
            name="date"
            value={editFormData.date}
            onChange={handleEditFormChange}
            />
        </td>

        <button type="submit" class="btn btn-square btn-secondary m-1">
            <i class="fa fa-check"/>
        </button>

        <button type="button" class="btn btn-square btn-secondary m-1" onClick={handleCancelClick}>
            <i class="fa fa-times"/>
        </button>


    </tr>
  )
}

export default EditableRow