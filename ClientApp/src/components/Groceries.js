import React, { Component } from 'react';

export class Groceries extends Component {
    constructor() {
        super();
        this.state = {
            groceries: []
        }
    }

    handleSubmit = (e) => {
        e.preventDefault();
        let groceries = this.state.groceries;
        let storeName = this.refs.storeName.value;
        let itemName = this.refs.itemName.value;
        let itemPrice = this.refs.itemPrice.value;

        let newGroceries = {
            "storeName" : storeName,
            "itemName" : itemName,
            "itemPrice" : itemPrice,
        }
        groceries.push(newGroceries);

        this.setState({
            groceries : groceries
        })

        this.refs.groceriesForm.reset();
    }

    render(){
        let groceries = this.state.groceries;
        return(
            <div>
                <form ref="groceriesForm">
                <label>Store Name</label>
                <input type="text" ref="storeName"/>
                <label>Item Name</label>
                <input type="text" ref="itemName"/>
                <label>Item Price</label>
                <input type="number" ref="itemPrice"/>
                <button onClick={e => this.handleSubmit(e)}>Save</button>
            </form>
            <table width="500">
                <tr>
                    <th>Store Name</th>
                    <th>Item Name</th>
                    <th>Item Price</th>
                </tr>
                {
                    groceries.map( (data, i) => 
                    <tr key={i}>
                        <td>{data.storeName}</td>
                        <td>{data.itemName}</td>
                        <td>{data.itemPrice}</td>
                    </tr> )
                }
            </table>
            </div>
        )
    }


}