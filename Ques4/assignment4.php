<?php

class assignment4 extends CI_Controller
{
	public function __construct() {
        parent::__construct();
	}

	public function index()
	{
		try
		{
			echo("Hello World");
			$this->load->model("assignment4model");
			//1. get users table and show it on a page
			$users_data["details"] = $this->assignment4model->getUserTable();
			$this->load->view('assignment4view',$users_data);
		}

		catch(Exception $e)
		{
			echo $e->getMessage();
		}
	}

	public  function fetchAccountData($id)
	{
		$this->load->model("assignment4model");
		$account_data = $this->assignment4model->getAccountData($id);
		header('Content-Type: application/json');
		echo json_encode($account_data);
	}
}

?>