import React, { Component } from 'react';
import SearchBar from './SearchBar';
import ProductsBox from './ProductsBox'

export class Search extends Component {

    render() {
        return(
            <div className="container-fluid pt-4 px-4">

                <div> 
                    <SearchBar placeHolder="Enter product"/>    
                </div>

                <div>
                    <ProductsBox/>
                </div>
               
            </div>
        )
    }
}